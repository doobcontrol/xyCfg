using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace xy.Cfg
{
    public class xyCfg
    {
        private JsonObject getCfgNode(string nName)
        {
            string jsonString = File.ReadAllText(cfgFile);
            JsonObject JsonObj
                = JsonSerializer.Deserialize<JsonObject>(jsonString);

            return (JsonObject)JsonObj[nName];
        }
        private JsonObject getCfgNode()
        {
            return getCfgNode(genNode);
        }

        private string getPar(string dType, string pName)
        {
            JsonObject cfgNode = getCfgNode(dType);
            return cfgNode[pName].ToString();
        }
        private string getPar(string pName)
        {
            return getPar(genNode, pName);
        }
        private void setPar(string dType, string pName, string pValue)
        {
            JsonObject cfgNode = getCfgNode(dType);
            cfgNode[pName] = pValue;
            string newJsonString = JsonSerializer.Serialize(cfgNode.Parent);
            File.WriteAllText(cfgFile, newJsonString);
        }
        private void setPar(string dType, Dictionary<string, string> pValues)
        {
            JsonObject cfgNode = getCfgNode(dType);
            foreach (var kvp in pValues)
            {
                cfgNode[kvp.Key] = kvp.Value;
            }
            string newJsonString = JsonSerializer.Serialize(cfgNode.Parent);
            File.WriteAllText(cfgFile, newJsonString);
        }
        private void setPar(string pName, string pValue)
        {
            setPar(genNode, pName, pValue);
        }
        private void setPar(Dictionary<string, string> pValues)
        {
            setPar(genNode, pValues);
        }

        private const string cfgFile = "xyCfg.json";
        private const string genNode = "genNode";

        static private xyCfg instance;
        static public void init(Dictionary<string,string> initCfgList)
        {
            Dictionary<string, Dictionary<string, string>> initCfg
                = new Dictionary<string, Dictionary<string, string>>();
            initCfg.Add(genNode, initCfgList);
            init(initCfg);
        }
        static public void init(
            Dictionary<string, Dictionary<string, string>> initCfg)
        {
            if (!File.Exists(cfgFile))
            {
                JsonObject JsonObj = new JsonObject();
                JsonObject keyValuePairs;
                foreach (var kvp in initCfg)
                {
                    keyValuePairs = new JsonObject();
                    foreach (var item in kvp.Value)
                    {
                        keyValuePairs[item.Key] = item.Value;
                    }
                    JsonObj.Add(kvp.Key, keyValuePairs);
                }

                string jsonString = JsonSerializer.Serialize(JsonObj);
                File.WriteAllText(cfgFile, jsonString);
            }
            instance = new xyCfg();
        }

        static public string get(string dType, string pName)
        {
            return instance.getPar(dType, pName);
        }
        static public string get(string pName)
        {
            return instance.getPar(genNode, pName);
        }
        static public void set(string dType, string pName, string pValue)
        {
            instance.setPar(dType, pName, pValue);
        }
        static public void set(string pName, string pValue)
        {
            instance.setPar(genNode, pName, pValue);
        }
        static public void set(string dType, Dictionary<string, string> pValues)
        {
            instance.setPar(dType, pValues);
        }
        static public void set(Dictionary<string, string> pValues)
        {
            instance.setPar(genNode, pValues);
        }
    }
}
