using System.Collections.Generic;

namespace OCT_Update
{
	/// <summary>
	/// 업데이트 파일과 버전 목록 클래스
	/// </summary>
	internal class FileVersionInfo
	{
		private Dictionary<string, string> _fileVersionList;

		/// <summary>
		/// Gets or sets the <see cref="System.String"/> with the specified file name.
		/// </summary>
		/// <value></value>
		public string this[string fileName]
		{
			get
			{
				if (_fileVersionList.ContainsKey(fileName))
				{
					return _fileVersionList[fileName];
				}
				else
					return "";
			}
			set
			{
				if (_fileVersionList.ContainsKey(fileName))
				{
					_fileVersionList[fileName] = value;
				}
				else
				{
					_fileVersionList.Add(fileName, value);
				}
			}
		}

		/// <summary>
		/// Gets the keys.
		/// </summary>
		/// <value>The keys.</value>
		public Dictionary<string, string>.KeyCollection Keys
		{
			get
			{
				return _fileVersionList.Keys;
			}
		}

		public void Add(string fileName, string version)
		{
			_fileVersionList.Add(fileName, version);
		}

		public void Remove(string fileName)
		{
			_fileVersionList.Remove(fileName);
		}

		public int Count
		{
			get { return _fileVersionList.Count; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileVersionInfo"/> class.
		/// </summary>
		public FileVersionInfo()
		{
			_fileVersionList = new Dictionary<string, string>();
		}
	}
}
