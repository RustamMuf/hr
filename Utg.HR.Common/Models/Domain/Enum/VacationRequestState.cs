namespace Utg.HR.Common.Models.Domain.Enum
{
	public enum VacationRequestState
	{
		None,
		Draft = 1,
		NeedLeadApprove = 2,
		LeedRejected = 3,
		NeedPersonalService = 4,
		PersonalServiceRejected = 5,
		ApprovedPersonalService = 6,
		AddedInVacations = 7,
		Rejected = 8
	}
}
