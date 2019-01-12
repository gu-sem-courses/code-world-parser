using System;
using System.Xml;
namespace parser
{
    public class SrcMLReader
    {

        public XmlElement[] GetClasses(XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            XmlNodeList jClasses = xDoc.DocumentElement.SelectNodes("//src:class[src:specifier[.!='abstract']]", nsm);
            XmlElement[] result = new XmlElement[jClasses.Count];

            for (int i = 0; i < jClasses.Count; i++)
            {
                XmlNode xClass = jClasses.Item(i);

                XmlElement javaClass, name, superClass;
                javaClass = xDoc.CreateElement("data");
                //add name
                name = GetClassName(xClass, xDoc, nsm);
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
            XmlNodeList jAttributes = classNode.SelectNodes("./src:block//src:decl_stmt//src:decl", nsm);
            XmlElement[] processedElements = new XmlElement[jAttributes.Count];

            //foreach (XmlNode attr in attributeList)
            for (int i = 0; i < jAttributes.Count; i++)
            {
                XmlNode attr = jAttributes.Item(i);

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
                        type = "null";
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
                processedElements[i] = attribute;
            }

            //loop through proccessed arrays and add them to root
            for(int i = 0; i < processedElements.Length; i++) {
                root.AppendChild(processedElements[i]); 
                }
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
        }

        public XmlElement GetSuperClass(XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm) {
            XmlElement result = xDoc.CreateElement("superclass");
            XmlNode super = classNode.SelectSingleNode("./descendant::src:super/src:extends", nsm);

            if ( super != null) {
                result.InnerText = super.SelectSingleNode("./src:name", nsm).InnerText;
                }
            else {
                result.InnerText = "";
            }
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
        }

        public void GetAssociations(String className, XmlNode root, XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm) {
            /*get all classes that call THIS className as a declarative statement AND where THIS class has an ancestor called unit*/
            XmlNodeList jClasses = xDoc.DocumentElement.SelectNodes("//src:class[parent::src:unit and .//src:decl_stmt//src:decl/src:type//src:name = \""+className+"\"]", nsm);

            if (jClasses.Count < 1)
            {
                XmlElement ass = xDoc.CreateElement("associations");
                ass.InnerText = "[]";
                root.AppendChild(ass);
            }
            else {
                foreach (XmlNode jClass in jClasses)
                {
                    try {
                        XmlElement ass = xDoc.CreateElement("associations");
                        ass.InnerText = GetClassName(jClass, xDoc, nsm).InnerText;
                        root.AppendChild(ass);
                    } catch {
                        XmlElement ass = xDoc.CreateElement("associations");
                        ass.InnerText = "null";
                        root.AppendChild(ass);
                    }
                }
            }
        }
    }
}
