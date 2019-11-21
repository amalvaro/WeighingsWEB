using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace AppConfiguration
{
	[XmlRoot(ElementName = "FieldCode")]
	public class FieldCode
	{
		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "FieldName")]
	public class FieldName
	{
		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "Caption")]
	public class Caption
	{
		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "AllowDBNull")]
	public class AllowDBNull
	{
		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}


	[XmlRoot(ElementName = "DefaultValue")]
	public class DefaultValue
	{
		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "TableCode")]
	public class TableCode
	{
		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "TableName")]
	public class TableName
	{
		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "TableCaption")]
	public class TableCaption
	{
		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "Field")]
	public class Field
	{
		[XmlElement(ElementName = "FieldCode")]
		public FieldCode FieldCode { get; set; }
		[XmlElement(ElementName = "FieldName")]
		public FieldName FieldName { get; set; }
		[XmlElement(ElementName = "Caption")]
		public Caption Caption { get; set; }
		[XmlElement(ElementName = "AllowDBNull")]
		public AllowDBNull AllowDBNull { get; set; }
		[XmlElement(ElementName = "DefaultValue")]
		public DefaultValue DefaultValue { get; set; }
	}

	[XmlRoot(ElementName = "Fields")]
	public class Fields
	{
		[XmlElement(ElementName = "Field")]
		public Field Field { get; set; }
	}

	[XmlRoot(ElementName = "WeighingTable")]
	public class WeighingTable
	{
		[XmlElement(ElementName = "TableCode")]
		public TableCode TableCode { get; set; }
		[XmlElement(ElementName = "TableName")]
		public TableName TableName { get; set; }
		[XmlElement(ElementName = "TableCaption")]
		public TableCaption TableCaption { get; set; }
		[XmlElement(ElementName = "Fields")]
		public List<Fields> Fields { get; set; }
	}

	[XmlRoot(ElementName = "WeighingTables")]
	public class WeighingTables
	{
		[XmlElement(ElementName = "WeighingTable")]
		public List<WeighingTable> WeighingTable { get; set; }
	}

	[XmlRoot(ElementName = "Database")]
	public class Database
	{
		[XmlElement(ElementName = "WeighingTables")]
		public WeighingTables WeighingTables { get; set; }
	}

	[XmlRoot(ElementName = "Configuration")]
	public class Configuration
	{
		[XmlElement(ElementName = "Database")]
		public Database Database { get; set; }
	}

}