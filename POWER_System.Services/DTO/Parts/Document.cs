using System.Xml.Serialization;

namespace POWER_System.Services.DTO.Parts;

[XmlRoot(ElementName = "Document")]
public class Document
{

    [XmlElement(ElementName = "Page")]
    public Page Page { get; set; }

    [XmlAttribute(AttributeName = "source_id")]
    public int SourceId { get; set; }

    [XmlText]
    public string Text { get; set; }
}