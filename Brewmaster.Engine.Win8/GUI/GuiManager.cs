using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Brewmaster.Engine.Win8.Framework;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Framework;
using BrewmasterEngine.GUI.Elements;
using Microsoft.Xna.Framework;
using Windows.Data.Xml.Dom;

namespace BrewmasterEngine.GUI
{
    public class GuiManager : GameManager
    {
        private Panel root;
        private string layoutName;

        #region Methods

        public void CreateHorizontalLayout(string name, Action<Element> build)
        {
            createLayout(Layout.Horizontal, name, build);
        }

        public void CreateVerticalLayout(string name, Action<Element> build)
        {
            createLayout(Layout.Vertical, name, build);
        }

        public void CreateAbsoluteLayout(string name, Action<Element> build)
        {
            createLayout(Layout.Absolute, name, build);
        }

        private void createLayout(Layout layout, string name, Action<Element> build)
        {
            layoutName = name;

            root = new Panel(1, layout)
            {
                Bounds = CurrentGame.Window.ClientBounds,
                ID = "r"
            };

            build(root);

            CurrentGame.Window.ClientSizeChanged -= reflowOnResize;
            CurrentGame.Window.ClientSizeChanged += reflowOnResize;
        }

        private void reflowOnResize(object sender, EventArgs e)
        {
            Reflow();
        }

        public void Reflow()
        {
            root.Bounds = CurrentGame.Window.ClientBounds;
            root.Reflow();
        }

        public override void Update(GameTime gameTime)
        {
            if (root == null) return;
                
            root.Reflow();
            root.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (root != null) root.Render(spriteBatch, gameTime);
        }

        public void SaveLayout()
        {
            Storage.WriteFile(layoutName + ".xml", ToXml());
        }

        public void LoadLayout()
        {
            LoadLayout(layoutName);
        }

        public void LoadLayout(string fileName)
        {
            fileName += ".xml";

            //if (!Storage.FileExists(fileName)) return;

            //var layout = Storage.ReadFile(fileName);


        }

        public string ToXml()
        {
            var doc = new XmlDocument();
            var xml = getXml(root, doc);
            doc.AppendChild(xml);

            return doc.GetXml();
        }

        private XmlElement getXml(Element element, XmlDocument doc)
        {
            var xml = doc.CreateElement(element.GetType().Name.ToLower());
            xml.SetAttribute("offset", element.Offset + "");
            xml.SetAttribute("span", element.Span + "");
            xml.SetAttribute("layout", element.Layout.ToString().ToLower() + "");

            foreach (var child in element.Children)
            {
                xml.AppendChild(getXml(child, doc));
            }

            return xml;
        }

        public override void Unload()
        {
            root = null;
        }

        #endregion

    }
}
