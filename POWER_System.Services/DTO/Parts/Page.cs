using System.Xml.Serialization;

namespace POWER_System.Services.DTO.Parts;

[XmlRoot(ElementName = "Page")]
public class Page
{

    [XmlElement(ElementName = "Header")]
    public object Header { get; set; }

    [XmlElement(ElementName = "ColumnHeader")]
    public List<ColumnHeader> ColumnHeader { get; set; }

    [XmlElement(ElementName = "Line")]
    public List<Line> Line { get; set; }

    [XmlElement(ElementName = "Footer")]
    public object Footer { get; set; }

    [XmlAttribute(AttributeName = "source_id")]
    public int SourceId { get; set; }

    [XmlText]
    public string Text { get; set; }
}