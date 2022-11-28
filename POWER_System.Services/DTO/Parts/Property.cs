using System.Xml.Serialization;

namespace POWER_System.Services.DTO.Parts;

[XmlRoot(ElementName = "Property")]
public class Property
{

    [XmlElement(ElementName = "PropertyName")]
    public string PropertyName { get; set; }

    [XmlElement(ElementName = "PropertyValue")]
    public string PropertyValue { get; set; }

    [XmlAttribute(AttributeName = "FormattingType")]
    public int FormattingType { get; set; }

    [XmlAttribute(AttributeName = "FormattingLength")]
    public int FormattingLength { get; set; }

    [XmlAttribute(AttributeName = "FormattingRAlign")]
    public int FormattingRAlign { get; set; }

    [XmlText]
    public string Text { get; set; }
}