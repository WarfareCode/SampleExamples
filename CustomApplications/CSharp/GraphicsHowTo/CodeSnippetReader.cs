using System.IO;
using System.Text;

namespace GraphicsHowTo
{
    /// <summary>
    /// Reads #region sections from a source file and returns 
    /// the using directives and code snippets.
    /// </summary>
    public class CodeSnippetReader
    {
        public CodeSnippetReader(string filePath)
        {
            m_filePath = filePath;
        }

        /// <summary>
        /// Returns the source code in the first '#region CodeSnippets' 
        /// section.  The string is cleaned up to display nicely in a 
        /// rich text box.
        /// </summary>
        public string Code
        {
            get
            {
                if (m_codeSnippet == null)
                {
                    m_codeSnippet = GetRegion("CodeSnippet");
                }

                return m_codeSnippet;
            }
        }

        /// <summary>
        /// Returns the source code in the first '#region UsingDirectives'
        /// section.  The string is cleaned up to display nicely in a 
        /// rich text box.
        /// </summary>
        public string UsingDirectives
        {
            get
            {
                if (m_usingDirectives == null)
                {
                    m_usingDirectives = GetRegion("UsingDirectives");
                }

                return m_usingDirectives;
            }
        }

        /// <summary>
        /// Returns the filename that contains the code snippet.
        /// </summary>
        public string FileName
        {
            get { return Path.GetFileName(m_filePath); }
        }

        /// <summary>
        /// Returns the filepath that contains the code snippet.
        /// </summary>
        public string FilePath
        {
            get { return m_filePath; }
        }

        /// <summary>
        /// Loads the source file, if it is not already loaded, and extracts
        /// the source code in the first region defined by the input string.
        /// </summary>
        private string GetRegion(string region)
        {
            if (m_fileContent == null)
            {
                m_fileContent = File.ReadAllLines(m_filePath);
            }

            StringBuilder result = new StringBuilder();

            // Find all occurrences of the specified region.

            string startRegionToken = "#region " + region;
            const string endRegionToken = "#endregion";

            bool insideRegion = false;
            int spacesToRemove = -1;
            foreach (string line in m_fileContent)
            {
                if (line.Contains(startRegionToken))
                {
                    // Start reading code snippet.
                    insideRegion = true;
                    spacesToRemove = -1;
                    continue;
                }

                if (line.Contains(endRegionToken))
                {
                    // Stop reading code snippet.
                    insideRegion = false;
                    continue;
                }

                if (!insideRegion)
                {
                    continue;
                }

                if (spacesToRemove < 0)
                {
                    // Compute number of spaces before code on the first non-blank line.  All following lines
                    // will have this many spaces removed from them to left-align the code in the rich text box.                    
                    bool isBlankLine = true;
                    int spaces = 0;
                    foreach (char c in line)
                    {
                        if (c == ' ' || c == '\t')
                        {
                            spaces++;
                        }
                        else
                        {
                            isBlankLine = false;
                            break;
                        }
                    }

                    if (isBlankLine)
                        continue;

                    spacesToRemove = spaces;
                }

                // Remove spaces from the front of the line, if the line is long enough.
                string lineOfCode = line;
                if (lineOfCode.Length > spacesToRemove)
                    lineOfCode = lineOfCode.Substring(spacesToRemove);

                result.Append(lineOfCode);
                result.Append('\n');
            }

            return result.ToString();
        }

        private readonly string m_filePath;
        private string[] m_fileContent;
        private string m_codeSnippet;
        private string m_usingDirectives;
    }
}