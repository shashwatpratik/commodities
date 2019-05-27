using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using commodities.Classes.Misc;
using commodities.Classes.BL;
using commodities.Models;

namespace commodities.ApiControllers
{
    /// <summary>
    /// Api for commodities related data.
    /// </summary>
    public class CommoditiesController : ApiController
    {
        /// <summary>
        /// Function to load root node.
        /// </summary>
        /// <returns>Returns Node as data if success, else error messega.</returns>
        [HttpGet]
        public MethodResultData<Node> GetNode()
        {
            try
            {
                return new BLForNode().GetNode("Crop commodities");
            }
            catch(Exception ex)
            {
                return new MethodResultData<Node>(ex.Message);
            }
        }
        /// <summary>
        /// Function to load node data based, based on path.
        /// </summary>
        /// <param name="nodePath">Path of node.</param>
        /// <returns>Returns Node as data if success, else error messega.</returns>
        [HttpGet]
        public MethodResultData<Node> GetNode(string nodePath)
        {
            try
            {
                return new BLForNode().GetNode(nodePath);
            }
            catch (Exception ex)
            {
                return new MethodResultData<Node>(ex.Message);
            }
        }
    }
}
