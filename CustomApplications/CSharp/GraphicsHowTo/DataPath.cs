using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Windows.Forms;

namespace AGI
{
    public enum DataPathRoot
    {
        TestData,
        Relative
    }

    [Serializable]
    public class DataPath
    {
        public DataPath(DataPathRoot root, string relative)
        {
            m_root = root;
            m_relative = relative;
        }

        public string FullPath
        {
            get
            {
                string root;
                if (m_root == DataPathRoot.TestData)
                {
                    // all users
                    string allUsers = System.Environment.GetEnvironmentVariable("ALLUSERSPROFILE");
                    // rest of path added a few lines below
                    string dataDir = allUsers;
                    bool notVista = (System.Environment.GetEnvironmentVariable("ProgramData") == null);
                    if (notVista)
                    {
                        dataDir = allUsers + @"\Application Data";
                    }
                    root = dataDir + @"\AGI\STK 11\";
                }
                else
                {
                    // Set a path to a directory containing the test resources.
                    root = Path.Combine(Application.StartupPath, "Data");
                }

                string relative = m_relative.Replace('\\', Path.DirectorySeparatorChar);
                relative = relative.Replace('/', Path.DirectorySeparatorChar);

                return Path.Combine(root, relative);
            }
        }

        public Uri Uri
        {
            get { return new Uri(FullAbsolutePath); }
        }

        public string FullAbsolutePath
        {
            get
            {
                string fullPath = FullPath;
                if (string.IsNullOrEmpty(fullPath))
                    return Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;

                if (!Path.IsPathRooted(fullPath))
                    return Path.Combine(Directory.GetCurrentDirectory(), fullPath);

                return fullPath;
            }
        }

        public override bool Equals(object obj)
        {
            DataPath dp = obj as DataPath;
            if (dp != null)
            {
                return FullPath == dp.FullPath;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return m_root.GetHashCode() ^ m_relative.GetHashCode();
        }

        private DataPathRoot m_root;
        private string m_relative;
    }
}