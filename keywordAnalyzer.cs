using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KeywordAnalyzer 
{
	public class KeywordAnalyzer 
	{
		public static void Main()
		{
			string link_word_file = @"linkwords_english.txt";
			List<string> linking_words = linkingWords(link_word_file);
			//createLinkwordList("words.txt", "linkwords_english.txt");
			List<Word> eksamen2011 = textParser("eksamen2011.txt", linking_words);
			List<Word> eksamen2012 = textParser("eksamen2012.txt", linking_words);
			//List<Word> eksamen2011 = textParser("eksamen2011.txt", linking_words);
			compare(eksamen2011, eksamen2012);
		}
		public static List<string> linkingWords(string path)
		{
			List<string> linkWords = new List<string>();
			StreamReader file = new StreamReader(path);
			string line;
			while((line = file.ReadLine()) != null)
			{
				linkWords.Add(line);
				//Console.WriteLine(line);
			}
			return linkWords;
		}
		
		public static List<Word> textParser(string fileName, List<string> link_words)
		{
			string line;
			StreamReader file = new StreamReader(fileName);
			List<Word> lWord = new List<Word>();
			
			while((line = file.ReadLine()) != null)
			{
				line = line.ToLower();
				string[] line_array = line.Split(' ');
				foreach (string w in line_array)
				{
					string sanitized = sanitizeString(w);
					//Console.WriteLine(sanitized);
					//var match = link_words.Contains(sanitized);
					//Console.WriteLine("match {0} , with {1}",match.ToString(), sanitized);
					if (link_words.Contains(sanitized) == false && sanitized != null){
						var res = lWord.Find(word => word.name == sanitized);
						if (res == null)
						{
							Word word = new Word();
							word.name = sanitized;
							word.counter = 1;
							word.context = line;
							lWord.Add(word);
						} else {
							res.counter++;
						}
					}
				}			
			}
			foreach (Word w in lWord){
				//Console.WriteLine("{0} funnet {1} ganger", w.name, w.counter);
				Console.WriteLine("{0} - {1}", w.name, w.counter);
				//Console.WriteLine("Context: '{0}'", w.context);
			}
			return lWord;
		}
		
		public static string sanitizeString (string str )
		{
			List<string> patterns = new List<string>() {"\\p{Sc}", "\\%", "\\.", "\\,", "\\?", "\\(", "\\)", "\\d+"};
			foreach (string pattern in patterns)
			{
				Regex rgx = new Regex(pattern);
				str = rgx.Replace(str, "");
				//Console.Write(str + " ");
			}
			//Console.WriteLine();
			//Console.WriteLine("returned {0}",str);
			if (str.Length <= 1)
				return null;
			else
				return str;
		}
		
		public static void createLinkwordList(string path, string outputFilename)
		{
			List<string> linkWords = new List<string>();
			StreamReader file = new StreamReader(path);
			string line;
			while((line = file.ReadLine()) != null)
			{
				line = line.ToLower();
				if (!linkWords.Contains(line)) 
				{
					linkWords.Add(line);
				} else {
					Console.WriteLine("Found duplicate ' {0} '", line);
				}
				
			}
			StreamWriter writer = new StreamWriter(outputFilename);
			foreach (string word in linkWords)
			{
				writer.WriteLine(word);
			}
		}
		
	}
	
	public class Word 
	{
		public string name { get; set; }
		public int counter { get; set; }
		public string context { get; set; }	
	}
}