using System;
using System.Xml;
namespace parser
{
    public class SrcMLReader
    {

        public XmlElement[] GetClasses(XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            Console.WriteLine("Class fetcher initiated");
            XmlNodeList classList = xDoc.DocumentElement.SelectNodes("//src:class[src:specifier[.!='abstract']]", nsm);
            XmlElement[] result = new XmlElement[classList.Count];
            Console.WriteLine("number of classes = " + classList.Count);
            Console.WriteLine("****************");

            for (int i = 0; i < classList.Count; i++)
            {
                XmlNode xClass = classList.Item(i);

                XmlElement javaClass, name, superClass;
                javaClass = xDoc.CreateElement("data");
                //add name
                name = GetClassName(xClass, xDoc, nsm);
                Console.WriteLine("\n"+name.InnerText + "****************" + (i+1));
                javaClass.AppendChild(name);
                //add attributes []
                GetAttributes(javaClass, xClass, xDoc, nsm);
                //add methods []
                GetMethods(javaClass, xClass, xDoc, nsm);
                //interfaces []
                GetInterfaces(javaClass, xClass, xDoc, nsm);
                //add subclasses []
                GetSubclasses(javaClass, xClass, xDoc, nsm);
                //add superclasses 
                superClass = GetSuperClass(xClass, xDoc, nsm);
                javaClass.AppendChild(superClass);
                //associations []
                GetAssociations(name.InnerText, javaClass, xClass, xDoc, nsm);

                //append result to an array
                result[i] = javaClass;
            }
            Console.WriteLine("Class fetcher complete");
            return result;
        }

        public XmlElement GetClassName(XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            XmlElement className = xDoc.CreateElement("name");
            className.InnerText = classNode.SelectSingleNode("./src:name[1]", nsm).InnerText;
            return className;
        }

        public void GetAttributes(XmlNode root, XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            //XmlNodeList attributeList = classNode.SelectNodes("./src:block//src:decl_stmt/src:decl[src:type/src:name [. = 'int']]", nsm);
            XmlNodeList declaredAttributes = classNode.SelectNodes("./src:block//src:decl_stmt//src:decl", nsm);
            XmlElement[] processedElements = new XmlElement[declaredAttributes.Count];

            //foreach (XmlNode attr in attributeList)
            for (int i = 0; i < declaredAttributes.Count; i++)
            {
                XmlNode attr = declaredAttributes.Item(i);
                //Console.WriteLine("actual attr");
                //Console.WriteLine(attr.InnerText);

                //retrive the nodes we need and store their value
                String type, name, accessModifier;
                XmlNode access, attrType;
                access = attr.SelectSingleNode("./src:specifier[1]", nsm);
                attrType = attr.SelectSingleNode("./src:type/src:name", nsm);

                if (access != null) {
                    accessModifier = access.InnerText;
                }
                else { accessModifier = "public"; //if there is no access modifier then its default = public
                }

                if(attrType != null) {
                    type = attrType.InnerText;
                }
                else {//if there is no type then check the previous node because it implies that there were a group of nodes declared at the same time
                    String reference = attr.SelectSingleNode("./src:type/@ref", nsm).InnerText;
                    if(reference == "prev") {
                        //although i-1 is risky (out of bounds) at the beggining, this case would never happn... but lets not take risks
                        if(i != 0) {
                            type = processedElements[i - 1].SelectSingleNode("./type").InnerText;
                        }
                        else {
                            type = "null";
                        }
                    }
                    else {
                        type = "n/a";
                    }
                }

                name = attr.SelectSingleNode("./src:name", nsm).InnerText;

                /*create a new element*/
                XmlElement attribute, accessModifierElement, typeElement, nameElement;

                /*accessmodifier*/
                accessModifierElement = xDoc.CreateElement("accessModifier");
                accessModifierElement.InnerText = accessModifier;

                /*type*/
                typeElement = xDoc.CreateElement("type");
                typeElement.InnerText = type;

                /*name*/
                nameElement = xDoc.CreateElement("name");
                nameElement.InnerText = name;

                /*attribute*/
                attribute = xDoc.CreateElement("attributes");
                attribute.AppendChild(accessModifierElement);
                attribute.AppendChild(typeElement);
                attribute.AppendChild(nameElement);

                Console.WriteLine("attr: "+attribute.SelectSingleNode("./name").InnerText);
                processedElements[i] = attribute;
            }

            //loop through proccessed arrays and add them to root
            for(int i = 0; i < processedElements.Length; i++) {
                root.AppendChild(processedElements[i]); 
                }
            Console.WriteLine("num of attr: " + declaredAttributes.Count);
        }

        public void GetMethods(XmlNode root, XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            XmlNodeList methodList = classNode.SelectNodes(".//src:function", nsm);

            //we only need type, name
            foreach (XmlNode meth in methodList)
            {
                String type, name, accessModifier;
                //retrive the nodes we need and store their value
                if (meth.SelectSingleNode("./src:specifier[1]", nsm) != null)
                {
                    accessModifier = meth.SelectSingleNode("./src:specifier[1]", nsm).InnerText;
                }
                else
                {
                    accessModifier = "public"; //if there is no access modifier then its default = public
                }
                type = meth.SelectSingleNode("./src:type/src:name", nsm).InnerText;
                name = meth.SelectSingleNode("./src:name", nsm).InnerText;

                //create elements
                XmlElement method, methodAccess, methodName, methodType;
                methodAccess = xDoc.CreateElement("accessModifier");
                methodAccess.InnerText = accessModifier;

                methodType = xDoc.CreateElement("returnType");
                methodType.InnerText = type; 

                methodName = xDoc.CreateElement("name");
                methodName.InnerText = name;

                method = xDoc.CreateElement("methods");
                method.AppendChild(methodAccess);
                method.AppendChild(methodType);
                method.AppendChild(methodName);

                /*append results*/
                root.AppendChild(method);
            }
            Console.WriteLine("num of methods: " + methodList.Count);
        }

        public void GetInterfaces(XmlNode root, XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm) {

            XmlNodeList jInterfaces = classNode.SelectNodes("./src:super//src:implements/src:name[last()]", nsm);

            foreach(XmlNode jInterface in jInterfaces) {

                XmlElement javaInterface = xDoc.CreateElement("interfaces");
                javaInterface.InnerText = jInterface.InnerText;

                root.AppendChild(javaInterface);
            }

            if (jInterfaces.Count < 1)
            {
                XmlElement javaInterface = xDoc.CreateElement("interfaces");
                javaInterface.InnerText = "none";

                root.AppendChild(javaInterface);
            }
            Console.WriteLine("num of interfaces: " + jInterfaces.Count);
        }

        public XmlElement GetSuperClass(XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm) {
            XmlElement result = xDoc.CreateElement("superclass");
            XmlNode super = classNode.SelectSingleNode("./descendant::src:super/src:extends", nsm);

            if ( super != null) {
                result.InnerText = super.SelectSingleNode("./src:name", nsm).InnerText;
                }
            else {
                result.InnerText = "none";
            }
            Console.WriteLine("superclass: " + result.InnerText);
            return result;
        }

        public void GetSubclasses(XmlNode root, XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            XmlNodeList subClasses = classNode.SelectNodes("//src:class[./src:super/src:extends/src:name [.='" + GetClassName(classNode, xDoc, nsm).InnerText + "']]", nsm);

            if(subClasses.Count > 0)
            {
                foreach (XmlNode sub in subClasses)
                {
                    XmlElement subClass = xDoc.CreateElement("subclasses");
                    subClass.InnerText = GetClassName(sub, xDoc, nsm).InnerText;

                    /*append results*/
                    root.AppendChild(subClass);
                }
            }
            else{
                XmlElement subClass = xDoc.CreateElement("subclasses");
                subClass.InnerText = "[]";

                /*append results*/
                root.AppendChild(subClass);
            }
            Console.WriteLine("num of superclasses: " + subClasses.Count);
        }

        public void GetAssociations(String className, XmlNode root, XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm) {
            /*get all classes that call THIS className as a declarative statement AND where THIS class has an ancestor called unit*/
            XmlNodeList jClasses = xDoc.DocumentElement.SelectNodes("//src:class[parent::src:unit and .//src:decl_stmt//src:decl/src:type//src:name = \""+className+"\"]", nsm);
            Console.WriteLine("num of ass: " + jClasses.Count);

            if (jClasses.Count < 1)
            {
                XmlElement ass = xDoc.CreateElement("associations");
                ass.InnerText = "none";
                root.AppendChild(ass);
            }
            else {
                foreach (XmlNode jClass in jClasses)
                {
                    //XmlElement ass = xDoc.CreateElement("associations");
                    //ass.InnerText = GetClassName(jClass, xDoc, nsm).InnerText;
                    //root.AppendChild(ass);
                    try {
                        XmlElement ass = xDoc.CreateElement("associations");
                        ass.InnerText = GetClassName(jClass, xDoc, nsm).InnerText;
                        root.AppendChild(ass);
                    } catch {
                        Console.WriteLine(jClass.InnerXml);
                        XmlElement ass = xDoc.CreateElement("associations");
                        ass.InnerText = "null";
                        root.AppendChild(ass);
                    }
                }
            }
        }
    }
}
