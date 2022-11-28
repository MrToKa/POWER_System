using System.Xml.Serialization;

namespace POWER_System.Services.DTO.Parts;

[XmlRoot(ElementName = "EplanLabelling")]
public class EplanLabelling
{

    [XmlElement(ElementName = "Document")]
    public Document Document { get; set; }

    [XmlAttribute(AttributeName = "version")]
    public double Version { get; set; }

    [XmlText]
    public string Text { get; set; }
}