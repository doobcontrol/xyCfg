using xy.Cfg;

namespace xyCfgExample
{
    public partial class Form1 : Form
    {
        private const string cfgFile = "xyCfg.json";
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clean up the config file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(cfgFile))
            {
                File.Delete(cfgFile);
            }
        }

        /// <summary>
        /// Initialize the config file if it does not exist 
        /// and display config values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> cfgValues = new Dictionary<string, string>
                {
                    { "name", "xyCfg" },
                    { "version", "1.0" },
                    { "author", "xy" }
                };
            xyCfg.init(cfgValues);
            ListLinear();
        }

        /// <summary>
        /// Set and display config values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {

            xyCfg.set("name", "xyCfgExample");
            xyCfg.set("version", "1.1");
            xyCfg.set("author", "dcontrol");
            ListLinear();
        }

        /// <summary>
        /// Display config values in a list box
        /// </summary>
        private void ListLinear()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add($"name: {xyCfg.get("name")}");
            listBox1.Items.Add($"version: {xyCfg.get("version")}");
            listBox1.Items.Add($"author: {xyCfg.get("author")}");
        }

        /// <summary>
        /// Initialize the config file with multiple sections if it does not exist
        /// and display config values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Dictionary<string, Dictionary<string, string>> cfgValues 
                = new Dictionary<string, Dictionary<string, string>>
                {
                    { "package", new Dictionary<string, string>{
                            { "name", "xyCfg" },
                            { "version", "1.0" },
                            { "author", "xy" }
                        } 
                    },
                    { "function", new Dictionary<string, string>{
                            { "language", "C#" },
                            { "platform", "Winfom" }
                        }
                    },
                    { "fame", new Dictionary<string, string>{
                            { "click", "1000" },
                            { "like", "3" }
                        }
                    }
                };
            xyCfg.init(cfgValues);
            ListCatalog();
        }

        /// <summary>
        /// Set and display config values in multiple sections
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            xyCfg.set("package", "name", "xyCfg_edited");
            xyCfg.set("package", "version", "1.0_edited");
            xyCfg.set("package", "author", "xy_edited");
            xyCfg.set("function", "language", "C#_edited");
            xyCfg.set("function", "platform", "Winfom_edited");
            xyCfg.set("fame", "click", "1000_edited");
            xyCfg.set("fame", "like", "3_edited");
            ListCatalog();
        }

        /// <summary>
        /// Display config values in multiple sections in a list box
        /// </summary>
        private void ListCatalog()
        {
            listBox2.Items.Clear();
            listBox2.Items.Add($"package.name: {xyCfg.get("package", "name")}");
            listBox2.Items.Add($"package.version: {xyCfg.get("package", "version")}");
            listBox2.Items.Add($"package.author: {xyCfg.get("package", "author")}");
            listBox2.Items.Add($"function.language: {xyCfg.get("function", "language")}");
            listBox2.Items.Add($"function.platform: {xyCfg.get("function", "platform")}");
            listBox2.Items.Add($"fame.click: {xyCfg.get("fame", "click")}");
            listBox2.Items.Add($"fame.like: {xyCfg.get("fame", "like")}");
        }
    }
}
