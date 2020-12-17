using System.Xml.Serialization;

namespace Sdr_FirmaInCloud.BL.FirmaInCloud.Domain
{
    [XmlRoot("EricsoftGuestData")]
    public class EricsoftGuestData
    {
        public Header Header { get; set; }
        public Body Body { get; set; }
    }
}
