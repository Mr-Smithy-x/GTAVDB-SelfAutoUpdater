using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTAVDB
{
    public class Latest
    {
        private static Dictionary<string, UInt32> natives = new Dictionary<string, uint>();
        private static Dictionary<string, UInt32> rpc = new Dictionary<string, uint>();

        private static bool isFinished = true;



        public static void LoadRPC(Form f, Listeners.OnNativeListener onNative)
        {
          
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Load RPC BASE From File";
                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    rpc.Clear();
                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        try
                        {
                            isFinished = false;
                            String backup = f.Text;
                            f.Invoke(new Action(() =>
                            {
                                f.Text = "Loading RPC BASE...";
                            }));
                            string file = System.IO.File.ReadAllText(ofd.FileName);
                            string[] s_native = file.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                            foreach (string native in s_native)
                            {
                                string[] hash = native.Split(new string[] { "=" }, StringSplitOptions.None);
                                if (hash.Length > 1)
                                {
                                    string name = hash[0].Trim();
                                    UInt32 offset = Convert.ToUInt32(hash[1].Trim(), 16);
                                    if (!rpc.ContainsKey(name))
                                    {
                                        rpc.Add(name, offset);
                                    }
                                }
                            }

                            isFinished = true;
                            f.Invoke(new Action(() =>
                            {

                                onNative.OnNativesLoaded("RPC BASE Loaded!");
                                f.Text = "RPC BASE Loaded";
                                new Thread(new ThreadStart(() =>
                                {
                                    Thread.Sleep(3000);
                                    f.Invoke(new Action(() =>
                                    {
                                        f.Text = backup;
                                    }));
                                })).Start();
                            }));
                        }
                        catch (Exception ex)
                        {

                            f.Invoke(new Action(() =>
                            {
                                onNative.OnNativesFailed("Failed To Load RPC BASE");
                            }));
                        }
                    }));
                    t.Start();

                }
                else
                {

                }
            }
        }

        /// <summary>
        /// Use as error Check, if its done loading the natives
        /// </summary>
        /// <returns>If done loading natives</returns>
        public static bool isOperationFinished()
        {
            return isFinished;
        }


        /// <summary>
        /// Load natives from file
        /// </summary>
        /// <param name="f">this form</param>
        /// <param name="onNative">public class Form1 : Form, GTAVDB.Listeners.OnNativeListener</param>
        public static void LoadNativesFromFile(Form f, Listeners.OnNativeListener onNative)
        {
          
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Load Natives From File";
                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {

                    natives.Clear();
                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        try
                        {
                            isFinished = false;
                            String backup = f.Text;
                            f.Invoke(new Action(() =>
                            {
                                f.Text = "Loading Natives...";
                            }));
                            string file = System.IO.File.ReadAllText(ofd.FileName);
                            string[] s_native = file.Split(new string[] { "," }, StringSplitOptions.None);
                            foreach (string native in s_native)
                            {
                                string[] hash = native.Split(new string[] { "=" }, StringSplitOptions.None);
                                if (hash.Length > 1)
                                {
                                    string name = hash[0].Trim();
                                    UInt32 offset = Convert.ToUInt32(hash[1].Trim(), 16);
                                    if (!natives.ContainsKey(name))
                                    {
                                        natives.Add(name, offset);
                                    }
                                }
                            }

                            isFinished = true;
                            f.Invoke(new Action(() =>
                            {

                                onNative.OnNativesLoaded("Natives Loaded!");
                                f.Text = "Natives Loaded";
                                new Thread(new ThreadStart(() =>
                                {
                                    Thread.Sleep(3000);
                                    f.Invoke(new Action(() =>
                                    {
                                        f.Text = backup;
                                    }));
                                })).Start();
                            }));
                        }
                        catch (Exception ex)
                        {

                            f.Invoke(new Action(() =>
                            {
                                onNative.OnNativesFailed("Failed To Load Natives");
                            }));
                        }
                    }));
                    t.Start();

                }
                else
                {

                }
            }
        }
       
        /// <summary>
        /// Select File To Get The Natives From
        /// </summary>
        public static void LoadNativesFromFile()
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Load Natives From File";
                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {

                    natives.Clear();
                    Thread t = new Thread(new ThreadStart(() =>
                    {
                            isFinished = false;
                            string file = System.IO.File.ReadAllText(ofd.FileName);
                            string[] s_native = file.Split(new string[] { "," }, StringSplitOptions.None);
                            foreach (string native in s_native)
                            {
                                string[] hash = native.Split(new string[] { "=" }, StringSplitOptions.None);
                                if (hash.Length > 1)
                                {
                                    string name = hash[0].Trim();
                                    UInt32 offset = Convert.ToUInt32(hash[1].Trim(), 16);
                                    if (!natives.ContainsKey(name))
                                    {
                                        natives.Add(name, offset);
                                    }
                                }
                            }
                     
                    }));
                    t.Start();

                }
                else
                {

                }
            }
        }
        /// <summary>
        /// Load Natives From URL
        /// </summary>
        /// <param name="f">this form </param>
        /// <param name="onNative">public class Form1 : Form, GTAVDB.Listeners.OnNativeListener</param>
        /// <param name="link">Provide Link to load natives</param>
        public static void LoadNatives(Form f, Listeners.OnNativeListener onNative,string link)
        {

            natives.Clear();
            Thread t = new Thread(new ThreadStart(() =>
            {
                try
                {
                    isFinished = false;
                    String backup = f.Text;
                    f.Invoke(new Action(() =>
                    {
                        f.Text = "Loading Natives...";
                    }));
                    WebClient wc = new WebClient();
                    string file = wc.DownloadString(link);
                    string[] s_native = file.Split(new string[] { "," }, StringSplitOptions.None);
                    foreach (string native in s_native)
                    {
                        string[] hash = native.Split(new string[] { "=" }, StringSplitOptions.None);
                        if (hash.Length > 1)
                        {
                            string name = hash[0].Trim();
                            UInt32 offset = Convert.ToUInt32(hash[1].Trim(), 16);
                            if (!natives.ContainsKey(name))
                            {
                                natives.Add(name, offset);
                            }
                        }
                    }
                    f.Invoke(new Action(() =>
                    {
                        onNative.OnNativesLoaded("Loaded");
                        f.Text = "Natives Loaded";
                        new Thread(new ThreadStart(() =>
                        {
                            Thread.Sleep(3000);
                            f.Invoke(new Action(() =>
                            {
                                f.Text = backup;
                                isFinished = true;
                            }));
                        })).Start();
                    }));
                }catch(Exception ex){
                    f.Invoke(new Action(() =>
                    {
                        onNative.OnNativesFailed("Failed To Load Natives");
                    }));
                }
            }));
            t.Start();
        }
        /// <summary>
        /// Load Native From URL
        /// </summary>
        /// <param name="link">Provide Link To Load Native, Must not have backslash comments, and must have commas at the end</param>
        public static void LoadNatives(string link)
        {

            natives.Clear();
            Thread t = new Thread(new ThreadStart(() =>
            {
                isFinished = false;
                WebClient wc = new WebClient();
                string file = wc.DownloadString(link);
                string[] s_native = file.Split(new string[] { "," }, StringSplitOptions.None);
                foreach(string native in s_native){
                    string[] hash = native.Split(new string[]{"="}, StringSplitOptions.None);
                    string name = hash[0].Trim();
                    UInt32 offset =Convert.ToUInt32(hash[1].Trim(),16);
                    natives.Add(name.ToUpper(), offset);  
                }
                isFinished = true;
            }));
            t.Start();
        }
        /// <summary>
        /// Retrives Address of native
        /// </summary>
        /// <param name="native">GIVE NATIVE NAME TO RETRIEVE THE ADDRESS</param>
        /// <returns>Hash</returns>
        public static uint GetNative(string native)
        {
            if (native.Length == 0) return 0x0;
            if (isFinished == true && natives.ContainsKey(native))
            {
                return natives[native];
            }
            else
            {
                return 0x0;
            }
        }
        /// <summary>
        /// Retrieves Address of native
        /// </summary>
        /// <param name="native">Using Native Enum, You can retrieve the native hash</param>
        /// <returns>Hash</returns>
        public static uint GetNative(Natives native)
        {
            if (isFinished == true)
            {
                return natives[Enum.GetName(typeof(Natives),native)];
            }
            else
            {
                return 0x0;
            }
        }

        /// <summary>
        /// Get RPC BASE
        /// </summary>
        /// <param name="rpc_hash">GET BASE FOR RPC</param>
        /// <returns>BASE</returns>
        public static uint GetRPCHash(string rpc_hash){
             if (rpc_hash.Length == 0) return 0x0;
            if (isFinished == true && rpc.ContainsKey(rpc_hash))
            {
                return rpc[rpc_hash];
            }
            else
            {
                return 0x0;
            }
            
        }
        public static uint GetRPCHash(RPC rpc_hash)
        {
            if (isFinished == true)
            {
                return rpc[Enum.GetName(typeof(RPC),rpc_hash)];
            }
            else
            {
                return 0x0;
            }
        }
    }
}
