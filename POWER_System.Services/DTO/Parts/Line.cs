using System.Xml.Serialization;

namespace POWER_System.Services.DTO.Parts;

[XmlRoot(ElementName = "Line")]
public class Line
{

    [XmlElement(ElementName = "Label")]
    public Label Label { get; set; }

    [XmlAttribute(AttributeName = "source_id")]
    public int SourceId { get; set; }

    [XmlAttribute(AttributeName = "separator")]
    public string Separator { get; set; }

    [XmlText]
    public string Text { get; set; }
}