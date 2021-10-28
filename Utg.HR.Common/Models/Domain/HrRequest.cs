using System;
using System.ComponentModel.DataAnnotations;

namespace Utg.HR.Common.Models.Domain
{
	public class HrRequest
	{
		[Key]
		public int HrRequestId { get; set; }
		public string Name { get; set; }
		public DateTime CreatedDate{ get; set; }
	}
}
