﻿using NClass.Translations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NClass.Core
{
    public abstract class Package : LanguageElement, IEntity, INestable, INestableChild
    {
        NestableHelper nestableHelper = null;
        NestableChildHelper nestableChildHelper = null;

        string name;

        public event SerializeEventHandler Serializing;
        public event SerializeEventHandler Deserializing;

        /// <exception cref="BadSyntaxException">
        /// The <paramref name="name"/> does not fit to the syntax.
        /// </exception>
        public Package(string name)
        {
            Initializing = true;
            Name = name;
            nestableHelper = new NestableHelper(this);
            nestableHelper.AddedNestedChild += (s, a) => Changed();
            nestableHelper.RemovedNestedChild += (s, a) => Changed();
            nestableChildHelper = new NestableChildHelper(this);
            nestableChildHelper.NestingParentChanged += (s, a) => Changed();
            Initializing = false;
        }

        public abstract Language Language
        {
            get;
        }

        public abstract string Stereotype
        {
            get;
        }

        /// <exception cref="BadSyntaxException">
        /// The <paramref name="value"/> does not fit to the syntax.
        /// </exception>
        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                string newName = Language.GetValidName(value, true);

                if (newName != name)
                {
                    name = newName;
                    Changed();
                }
            }
        }

        public EntityType EntityType
        {
            get { return EntityType.Package; }
        }

        void ISerializableElement.Serialize(XmlElement node)
        {
            Serialize(node);
        }

        void ISerializableElement.Deserialize(XmlElement node)
        {
            Deserialize(node);
        }

        public abstract Package Clone();

        /// <exception cref="ArgumentNullException">
        /// <paramref name="node"/> is null.
        /// </exception>
        internal void Serialize(XmlElement node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            XmlElement child;

            child = node.OwnerDocument.CreateElement("Name");
            child.InnerText = Name;
            node.AppendChild(child);

            OnSerializing(new SerializeEventArgs(node));
        }

        /// <exception cref="BadSyntaxException">
        /// An error occured while deserializing.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The XML document is corrupt.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="node"/> is null.
        /// </exception>
        internal void Deserialize(XmlElement node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            RaiseChangedEvent = false;
            XmlElement nameChild = node["Name"];
            if (nameChild != null)
                Name = nameChild.InnerText;

            RaiseChangedEvent = true;
            OnDeserializing(new SerializeEventArgs(node));
        }

        private void OnSerializing(SerializeEventArgs e)
        {
            if (Serializing != null)
                Serializing(this, e);
        }

        private void OnDeserializing(SerializeEventArgs e)
        {
            if (Deserializing != null)
                Deserializing(this, e);
        }

        protected virtual void CopyFrom(Package package)
        {
            name = package.name;
        }

        public abstract string FullName
        {
            get;
        }

        public override string GetDeclaration()
        {
            //TODO: In order to get the correct declaration of package (namespace) a RootNamespace is required
            //for the process. It cannot be provided easy way here.
            //Package would need to "know" the model it's in. Model could provide RootNamespace somehow.
            throw new NotImplementedException("Package.GetDeclaration");
        }

        public override string ToString()
        {
            return Name;
        }

        #region INestable Implementation

        public IEnumerable<INestableChild> NestedChilds
        {
            get { return nestableHelper.NestedChilds; }
        }

        public void AddNestedChild(INestableChild type)
        {
            nestableHelper.AddNestedChild(type);
        }

        public void RemoveNestedChild(INestableChild type)
        {
            nestableHelper.RemoveNestedChild(type);
        }

        public bool IsNestedAncestor(INestableChild type)
        {
            return nestableHelper.IsNestedAncestor(type);
        }

        #endregion

        #region INestableChild Implementation

        public virtual INestable NestingParent
        {
            get { return nestableChildHelper.NestingParent; }
            set { nestableChildHelper.NestingParent = value; }
        }

        #endregion
    }
}