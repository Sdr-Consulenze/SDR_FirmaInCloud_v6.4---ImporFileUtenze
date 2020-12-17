using System;

namespace Sdr_FirmaInCloud.BL.FirmaInCloud
{
	public class Document
    {
		public int KEY_DOC { get; set; }
		public int? TYP_DOC { get; set; }
		public string NUM_DOC { get; set; }
		public DateTime ISS_DOC { get; set; }
		public string CITY_DOC { get; set; }
		public string ZIP_CODE { get; set; }
		public string ID_DOC { get; set; }
	}
}
