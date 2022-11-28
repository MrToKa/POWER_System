using System.Xml.Serialization;

namespace POWER_System.Services.DTO.Parts;

[XmlRoot(ElementName = "Label")]
public class Label
{

    [XmlElement(ElementName = "Property")]
    public List<Property> Property { get; set; }

    [XmlAttribute(AttributeName = "source_id")]
    public int SourceId { get; set; }

    [XmlText]
    public string Text { get; set; }
}