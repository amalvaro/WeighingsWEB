using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AppConfiguration;

namespace WeighingsWEB
{
	

	public class Manager
	{
		public Configuration configuration { get; private set; }
		public Manager()
		{
			Init();
		}

		private void Init()
		{

			using (TextReader textReader = new StreamReader(
				new FileStream("./AppConfig/хмао-v5-test.xml", FileMode.OpenOrCreate)))
			{
				configuration = (new XmlSerializer(typeof(Configuration)))
					.Deserialize(textReader) as Configuration;
			}

		}

	}
}
