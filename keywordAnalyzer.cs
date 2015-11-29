using System;
using System.IO;
using System.Collections.Generic;

namespace KeywordAnalyzer 
{
	public class KeywordAnalyzer 
	{
		public static void Main()
		{
			//string link_word_file = @"linkwords_english.txt";
			//List<string> linking_words = linkingWords(link_word_file);
			//createLinkwordList("words.txt", "linkwords_english.txt");
		}
		public static List<string> linkingWords(string path)
		{
			List<string> linkWords = new List<string>();
			StreamReader file = new StreamReader(path);
			string line;
			while((line = file.ReadLine()) != null)
			{
				linkWords.Add(line);
			}
			return linkWords;
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
	
	public class Words 
	{
		public string word { get; set; }
		public int counter { get; set; }	
	}
}