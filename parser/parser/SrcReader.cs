using Newtonsoft.Json;
using System;
using System.Xml;
namespace parser
{
    public class SrcReader
    {
        public XmlElement GetClasses(XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            XmlNodeList classList = xDoc.DocumentElement.SelectNodes("//src:class[src:specifier[.!='abstract']]", nsm);
            Console.WriteLine("number of classes = " + classList.Count);

            XmlElement result = xDoc.CreateElement("classes");

            foreach (XmlNode xClass in classList)
            {

                /*
                what does a class have?:
                - class name
                - class extends || implements
                - attributes && components
                - methods
                - */

                Console.WriteLine("****************");
                XmlElement name, attributes, methods, superClass, subClass, javaClass;
                name = GetClassName(xClass, xDoc, nsm);
                attributes = GetAttributes(xClass, xDoc, nsm);
                methods = GetMethods(xClass, xDoc, nsm);
                superClass = GetSuperClass(xClass, xDoc, nsm);
                subClass = GetSubclasses(xClass, xDoc, nsm);

                javaClass = xDoc.CreateElement("class");
                javaClass.AppendChild(name);
                javaClass.AppendChild(attributes);
                javaClass.AppendChild(methods);
                javaClass.AppendChild(superClass);
                javaClass.AppendChild(subClass);
                Console.WriteLine("****************");

                result.AppendChild(javaClass);
            }

            return result;
        }

        public XmlElement GetClassName(XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            XmlElement className = xDoc.CreateElement("name");
            className.InnerText = classNode.SelectSingleNode("src:name[1]", nsm).InnerText;
            Console.WriteLine("class: "+className.InnerText);
            return className;
        }

        public XmlElement GetAttributes(XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            //XmlNodeList attributeList = classNode.SelectNodes("./src:block//src:decl_stmt/src:decl[src:type/src:name [. = 'int']]", nsm);
            XmlNodeList declarations = classNode.SelectNodes("./src:block//src:decl_stmt", nsm);
            XmlNodeList attributeList = classNode.SelectNodes("./src:block//src:decl_stmt//src:decl", nsm);
            XmlElement result = xDoc.CreateElement("attributes");
            Console.WriteLine("num of delarations = " + declarations.Count);
            Console.WriteLine("num of attributes= " + attributeList.Count);
            //we only need type, name;
            foreach (XmlNode attr in attributeList)
            {
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
                        type = result.LastChild.SelectSingleNode("./type").InnerText;
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
                attribute = xDoc.CreateElement("attribute");
                attribute.AppendChild(accessModifierElement);
                attribute.AppendChild(typeElement);
                attribute.AppendChild(nameElement);

                /*append results*/
                result.AppendChild(attribute);
            }
            return result;
        }

        public XmlElement GetMethods(XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            XmlNodeList methodList = classNode.SelectNodes(".//src:function", nsm);
            XmlElement result = xDoc.CreateElement("methods");
            Console.WriteLine("num of methHeads " + methodList.Count);

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

                method = xDoc.CreateElement("method");
                method.AppendChild(methodAccess);
                method.AppendChild(methodType);
                method.AppendChild(methodName);

                /*append results*/
                result.AppendChild(method);
            }
            return result;
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
            return result;
        }

        public XmlElement GetSubclasses(XmlNode classNode, XmlDocument xDoc, XmlNamespaceManager nsm)
        {
            XmlElement result = xDoc.CreateElement("subClasses");
            String className = GetClassName(classNode, xDoc, nsm).InnerText;
            XmlNodeList subs = classNode.SelectNodes("//src:class[./src:super/src:extends/src:name [.='" + className + "']]", nsm);

            Console.WriteLine("num of subclasses = " + subs.Count);

            foreach (XmlNode sub in subs) {
                XmlElement subClass = xDoc.CreateElement("subClass");
                subClass.InnerText = GetClassName(sub, xDoc, nsm).InnerText;

                /*append results*/
                result.AppendChild(subClass);
            }
            return result;
        }

    }
}
