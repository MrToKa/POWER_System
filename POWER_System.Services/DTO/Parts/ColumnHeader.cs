using System.Xml.Serialization;

namespace POWER_System.Services.DTO.Parts;

[XmlRoot(ElementName = "ColumnHeader")]
public class ColumnHeader
{

    [XmlElement(ElementName = "PropertyName")]
    public string PropertyName { get; set; }

    [XmlAttribute(AttributeName = "DataType")]
    public string DataType { get; set; }

    [XmlText]
    public string Text { get; set; }
}