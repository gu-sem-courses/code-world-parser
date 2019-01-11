using System;
using System.Xml;
namespace parser
{
    public class SrcMLFilter
    {

        //public XmlElement[] GetClasses(XmlNode target, XmlDocument xDoc, XmlNamespaceManager nsm)
        public void GetClasses(XmlNode target, XmlDocument xDoc, XmlNamespaceManager nsm)
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
                //add superclasses 
                superClass = GetSuperClass(xClass, xDoc, nsm);
                javaClass.AppendChild(superClass);
                //add subclasses []
                GetSubclasses(javaClass, xClass, xDoc, nsm);
                //associations []
                GetAssociations(name.InnerText, javaClass, xClass, xDoc, nsm);
                //append result to an array
                result[i] = javaClass;
                target.AppendChild(javaClass);
                Console.WriteLine(javaClass.OuterXml);
            }
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

            for (int i = 0; i < jAttributes.Count; i++)
            {
                XmlNode attr = jAttributes.Item(i);

                //retrive the nodes we need and store their value
                String type, name, accessModifier;
                XmlNode access, attrType;
                access = attr.SelectSingleNode("./src:specifier[1]", nsm);
                attrType = attr.SelectSingleNode("./src:type/src:name", nsm);

                //access modifier
                if (access != null)
                {
                    accessModifier = access.InnerText;
                }
                else
                {
                    accessModifier = "public"; //if there is no access modifier then its default = public
                }

                //type
                if (attrType != null)
                {
                    type = attrType.InnerText;
                }
                else
                {//if there is no type then check the previous node because it implies that there were a group of nodes declared 
                    if (i != 0)
                    {
                        type = processedElements[i - 1].SelectSingleNode("./type").InnerText;
                    }
                    else
                    {
                        type = "null";
                    }
                }

                //name
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
                root.AppendChild(processedElements[i]);
            }
        }

        public void GetMethods(XmlNode root, XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            XmlNodeList jMethods = classNode.SelectNodes(".//src:function", nsm);

            //we only need type, name
            foreach (XmlNode meth in jMethods)
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

        public XmlElement GetSuperClass(XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            XmlElement result = xDoc.CreateElement("superclass");
            XmlNode super = classNode.SelectSingleNode("./descendant::src:super/src:extends", nsm);

            if (super != null)
            {
                result.InnerText = super.SelectSingleNode("./src:name", nsm).InnerText;
            }
            else
            {
                result.InnerText = "";
            }
            return result;
        }

        public void GetSubclasses(XmlNode root, XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            XmlNodeList subClasses = classNode.SelectNodes("//src:class[./src:super/src:extends/src:name [.='" + GetClassName(classNode, xDoc, nsm).InnerText + "']]", nsm);
            XmlElement subClass = xDoc.CreateElement("subclasses");
            if (subClasses.Count > 0)
            {
                foreach (XmlNode sub in subClasses)
                {
                    subClass.InnerText = GetClassName(sub, xDoc, nsm).InnerText;
                }
            }
            else {
                subClass.InnerText = "[]";
            }
            /*append results*/
            root.AppendChild(subClass);
        }

        public void GetAssociations(String className, XmlNode root, XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            /*get all classes that call THIS className as a declarative statement AND where THIS class has an ancestor called unit*/
            XmlNodeList jClasses = xDoc.DocumentElement.SelectNodes("//src:class[parent::src:unit and .//src:decl_stmt//src:decl/src:type//src:name = \"" + className + "\"]", nsm);
            XmlElement ass = xDoc.CreateElement("associations");

            if (jClasses.Count >= 1)
            {
                foreach (XmlNode jClass in jClasses)
                {
                    ass.InnerText = GetClassName(jClass, xDoc, nsm).InnerText;
                }
            }
            else {
                ass.InnerText = "[]";
            }
            root.AppendChild(ass);
        }
    }
}
