# xyCfg

A simple library for application configuration management. Support multiple sections configuration.

## Example

Detail in project *xyCfgExample*

### Initialize

Set configuration data on application start up:

Dictionary<string, string> cfgValues = new Dictionary<string, string>

&nbsp;   {

&nbsp;       { "name", "xyCfg" },

&nbsp;       { "version", "1.0" },

&nbsp;       { "author", "xy" }

&nbsp;   };

xyCfg.init(cfgValues);

For multiple sections configuration:

Dictionary<string, Dictionary<string, string>> cfgValues 

&nbsp;   = new Dictionary<string, Dictionary<string, string>>

&nbsp;   {

&nbsp;       { "package", new Dictionary<string, string>{

&nbsp;               { "name", "xyCfg" },

&nbsp;               { "version", "1.0" },

&nbsp;               { "author", "xy" }

&nbsp;           } 

&nbsp;       },

&nbsp;       { "function", new Dictionary<string, string>{

&nbsp;               { "language", "C#" },

&nbsp;               { "platform", "Winfom" }

&nbsp;           }

&nbsp;       },

&nbsp;       { "fame", new Dictionary<string, string>{

&nbsp;               { "click", "1000" },

&nbsp;               { "like", "3" }

&nbsp;           }

&nbsp;       }

&nbsp;   };

xyCfg.init(cfgValues);

### Set config value

xyCfg.set("name", "xyCfgExample");

For multiple sections configuration:

xyCfg.set("package", "name", "xyCfg\_edited");

### Get config value

xyCfg.get("name");

For multiple sections configuration:

xyCfg.get("package", "name");





