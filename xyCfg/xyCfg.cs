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

        private object? getPar(string dType, string pName)
        {
            JsonObject cfgNode = getCfgNode(dType);
            var pValue = cfgNode[pName];
            if (pValue is JsonValue)
            {
                return cfgNode[pName].GetValue<string>();
            }
            else if (pValue is JsonArray)
            {
                List<string> list = new List<string>();
                foreach (var item in (JsonArray)pValue)
                {
                    list.Add(item.ToString());
                }
                return list;
            }
            return null;
        }
        private object? getPar(string pName)
        {
            return getPar(genNode, pName);
        }
        private void setPar(string dType, string pName, object pValue)
        {
            JsonObject cfgNode = getCfgNode(dType);
            if(pValue is string)
            {
                cfgNode[pName] = pValue.ToString();
            }
            else if(pValue is List<string>)
            {
                JsonArray jsonArray = new JsonArray();
                foreach (var item in (List<string>)pValue)
                {
                    jsonArray.Add(item);
                }
                cfgNode[pName] = jsonArray;
            }

            string newJsonString = JsonSerializer.Serialize(cfgNode.Parent);
            File.WriteAllText(cfgFile, newJsonString);
        }
        private void setPar(string dType, Dictionary<string, object> pValues)
        {
            JsonObject cfgNode = getCfgNode(dType);
            foreach (var kvp in pValues)
            {
                if (kvp.Value is string)
                {
                    cfgNode[kvp.Key] = kvp.Value.ToString();
                }
                else if (kvp.Value is List<string>)
                {
                    JsonArray jsonArray = new JsonArray();
                    foreach (var item in (List<string>)kvp.Value)
                    {
                        jsonArray.Add(item);
                    }
                    cfgNode[kvp.Key] = jsonArray;
                }
            }
            string newJsonString = JsonSerializer.Serialize(cfgNode.Parent);
            File.WriteAllText(cfgFile, newJsonString);
        }
        private void setPar(string pName, object pValue)
        {
            setPar(genNode, pName, pValue);
        }
        private void setPar(Dictionary<string, object> pValues)
        {
            setPar(genNode, pValues);
        }


        private static string cfgFile = "xyCfg.json";
        private const string genNode = "genNode";

        static private xyCfg instance;
        static public void init(Dictionary<string, object> initCfgList, string? cFile = null)
        {
            if (cFile != null)
            {
                cfgFile = cFile;
            }
            Dictionary<string, Dictionary<string, object>> initCfg
                = new Dictionary<string, Dictionary<string, object>>();
            initCfg.Add(genNode, initCfgList);
            init(initCfg);
        }
        static public void init(
            Dictionary<string, Dictionary<string, object>> initCfg, string? cFile = null)
        {
            if(cFile != null)
            {
                cfgFile = cFile;
            }
            if (!File.Exists(cfgFile))
            {
                JsonObject JsonObj = new JsonObject();
                JsonObject keyValuePairs;
                foreach (var kvp in initCfg)
                {
                    keyValuePairs = new JsonObject();
                    foreach (var item in kvp.Value)
                    {
                        if (item.Value is string)
                        {
                            keyValuePairs[item.Key] = item.Value.ToString();
                        }
                        else if (item.Value is List<string>)
                        {
                            JsonArray jsonArray = new JsonArray();
                            foreach (var v in (List<string>)item.Value)
                            {
                                jsonArray.Add(v);
                            }
                            keyValuePairs[item.Key] = jsonArray;
                        }
                    }
                    JsonObj.Add(kvp.Key, keyValuePairs);
                }

                string jsonString = JsonSerializer.Serialize(JsonObj);
                File.WriteAllText(cfgFile, jsonString);
            }
            instance = new xyCfg();
        }

        static public object? get(string dType, string pName)
        {
            return instance.getPar(dType, pName);
        }
        static public object? get(string pName)
        {
            return instance.getPar(genNode, pName);
        }
        static public void set(string dType, string pName, object pValue)
        {
            instance.setPar(dType, pName, pValue);
        }
        static public void set(string pName, object pValue)
        {
            instance.setPar(genNode, pName, pValue);
        }
        static public void set(string dType, Dictionary<string, object> pValues)
        {
            instance.setPar(dType, pValues);
        }
        static public void set(Dictionary<string, object> pValues)
        {
            instance.setPar(genNode, pValues);
        }
    }
}
