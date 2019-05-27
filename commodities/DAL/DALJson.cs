using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using commodities.Models;
using Newtonsoft.Json.Linq;

namespace commodities.DAL
{
    /// <summary>
    /// Class for getting data from json1.
    /// </summary>
    public class DALJson
    {
        private JObject _jsonDataObject;
        private int _jsonFileId;
        /// <summary>
        /// Constructor for Data access layer of json.
        /// </summary>
        /// <param name="jsonFileId">File id of json</param>
        public DALJson(int jsonFileId)
        {
            _jsonFileId = jsonFileId;
            _jsonDataObject = JObject.Parse(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data/", $"level{_jsonFileId}.json")));

        }
        /// <summary>
        /// Function to check if root node exists.
        /// </summary>
        /// <param name="name">Name of Node.</param>
        /// <param name="parentName">Name of Parent Node, if any else empty.</param>
        /// <returns>Returns true,false based on check.</returns>
        public bool CheckRootExists(string name, string parentName)
        {
            //If root element of very fisrt json then it's not requried to check the parent.
            if (!parentName.Equals(String.Empty))
            {
                var parentNode = new DALJson(_jsonFileId - 1).GetTermNodes(parentName);
                if (!(parentNode.ChildNodes.Any(s => s.Name.Equals(name))))
                    return false;
            }
            if (((string)_jsonDataObject["name"]).Equals(name))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Function to get root node.
        /// </summary>
        /// <returns>Returns Root node with its child nodes.</returns>
        public Node GetRootChildrens(string nodeName)
        {
            Node node = new Node() { Name = nodeName };

            IList<Node> childNodes = new List<Node>();
            foreach (var term in (JArray)_jsonDataObject["termsrelation"])
            {
                string name = (string)term["rel"];
                childNodes.Add(new Node { Name = name });
            }
            node.ChildNodes = childNodes;
            return node;
        }
        /// <summary>
        /// Function to check if rel node exists.
        /// </summary>
        /// <param name="relName">Rel node name.</param>
        /// <param name="parentName">Parent node name.</param>
        /// <returns>Returns true,false based on check.</returns>
        public bool CheckTermNodesExistsForRel(string relName, string parentName)
        {
            var parentNode = GetRootChildrens(parentName);
            if (!(parentNode.ChildNodes.Any(s => s.Name.Equals(relName))))
                return false;
            var json = _jsonDataObject["termsrelation"];
            var termRelation = json.FirstOrDefault(fd => ((string)fd["rel"]).Equals(relName));
            if (termRelation == null)
                return false;
            return true;
        }
        /// <summary>
        /// Function to get rel's child nodes.
        /// </summary>
        /// <param name="relName">Node Name</param>
        /// <returns>Returns node with child nodes.</returns>
        public Node GetTermNodes(string relName)
        {
            Node node = new Node() { Name = relName };
            var json = _jsonDataObject["termsrelation"];
            var termRelation = json.FirstOrDefault(fd => ((string)fd["rel"]).Equals(relName));
            if (termRelation == null)
                return node;
            node.Name = (string)termRelation["rel"];
            IList<Node> childNodes = new List<Node>();
            foreach (var term in (JArray)termRelation["terms"])
            {
                childNodes.Add(new Node { Name = (string)term["name"] });
            }
            node.ChildNodes = childNodes;
            return node;
        }
    }
}