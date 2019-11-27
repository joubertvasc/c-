/*
 * NETShots - by Alessandro Fragnani
 * FileManager.cs
 * Created: 28 march 2005
 * 
 * A Manager class for Folder/Files operation
 * 
 */ 

using System;
using System.Collections;
using System.IO;

namespace NetShots
{
	/// <summary>
	/// Summary description for FileManager.
	/// 
	/// It is responsible for operations on Files and Folders
	/// - Folder - retrieve subfolders and files to get all the images that will be 
	///            shooted
	/// - File   - decides if a file is a valid image
	///          - loads the image, retuning an Image object to be attached to the 
	///            pictureBrowser
	/// </summary>
	public class FileManager
	{
		public FileManager()
		{
			//
			// TODO: Add constructor logic here
			//
			needUpdate = false;
		}

		/// <summary>
		/// indicates if some directory has been deleted
		/// </summary>
		private bool hasDeleted = false;

		/// <summary>
		/// newly added directories
		/// </summary> 
		private ArrayList newDirectories = new ArrayList();

		/// <summary>
		/// all active directories
		/// </summary> 
		private ArrayList directories = new ArrayList();

		/// <summary>
		/// The array of images that has been processed for a specified directory
		/// </summary>
		public ArrayList images = new ArrayList();


		/// <summary>
		/// Indicates that something has changed, so a UpdateDirectories should be executed
		/// </summary>
		public bool needUpdate = false;

		

		/// <summary>
		/// Process, recursivelly, each directory and its files
		/// </summary>
		/// <param name="targetDirectory"></param>
		public void ProcessDirectory(string targetDirectory) 
		{
			// Process the list of files found in the directory
			string [] fileEntries = Directory.GetFiles(targetDirectory);
			foreach(string fileName in fileEntries)
				ProcessFile(fileName);

			// Recurse into subdirectories of this directory
			string [] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
			foreach(string subdirectory in subdirectoryEntries)
				ProcessDirectory(subdirectory);
		}
        

		/// <summary>
		/// Process each file, accepting only .JGP and .GIF images, adding to images
		/// ArrayList
		/// </summary>
		/// <param name="path"></param> 
		public void ProcessFile(string path) 
		{
			//if (FileAttributes.Directory in File.GetAttributes(path) 
			//Console.WriteLine("Processed file '{0}'.", path);       
			FileInfo fi = new FileInfo(path);
			if ((fi.Extension.ToUpper() == ".JPG") || (fi.Extension.ToUpper() == ".GIF")) 
			{
				images.Add(path);
			}
			
		}


		/// <summary>
		/// Clear the FileManager
		/// </summary>
		public void Clear() 
		{
			ClearImageArray();

			newDirectories.Clear();
			directories.Clear();
		}


		/// <summary>
		/// Clear the image array
		/// </summary>
		public void ClearImageArray() 
		{
			images.Clear();
		}


		/// <summary>
		/// Add a directory to be processed later
		/// </summary>
		/// <param name="directory"></param>
		public void AddDirectory(string directory) 
		{						
			System.Collections.IEnumerator myEnumerator = newDirectories.GetEnumerator();
			bool achou = false;
			while (myEnumerator.MoveNext()) 
			{
				if (((string)myEnumerator.Current).ToLower().Equals(directory.ToLower())) 
				{
					achou = true;
				}
			}

			if (!achou) 
			{
				newDirectories.Add(directory);
				needUpdate = true;
			}
		}


		public void DelDirectory(string directory) 
		{
			newDirectories.Remove(directory);
			directories.Remove(directory);

			hasDeleted = true;
			needUpdate = true;
		}


		/// <summary>
		/// Update the directories based on the existing newDirectories
		/// </summary>
		public void UpdateDirectories() 
		{
			// if some has been deleted, must do a full refresh
			if (hasDeleted) 
			{
				newDirectories.AddRange(directories);
				images.Clear();
				directories.Clear();
				hasDeleted = false;
			}

			foreach(string directory in newDirectories) 
			{
				ProcessDirectory(directory);
				directories.Add(directory);
			}

			newDirectories.Clear();
			needUpdate = false;
		}
	}
}
