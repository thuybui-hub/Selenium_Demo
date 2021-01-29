using SeleniumCSharp.Core.ElementWrapper;
using JitbitHelpdeskAutomation.DataObject;
using JitbitHelpdeskAutomation.PageObject.Common;

namespace JitbitHelpdeskAutomation.PageObject.UserDetails
{
    public class UserDetailsPage: CommonPage
    {
        // Properties
        private readonly Table tblUserDetail;   

        public UserDetailsPage()
        {
            tblUserDetail = new Table("//table[@class='lightbg outerroundedbox']");            
        }

        #region Actions
        public string GetTableCellValue(int rowIndex=0, int colIndex=0)
        {
            Table tblUserInfo = new Table("//table[@class='lightbg outerroundedbox']//tr[{0}]/td[{1}]");
            tblUserInfo.Format(rowIndex, colIndex);
            return tblUserInfo.GetText();
        }
        #endregion

        #region Check points
        public bool IsSubmiterInfoCorrect(SubmiterData submiter)
        {
            string firtname = GetTableCellValue(4, 2);
            string lastname = GetTableCellValue(5, 2);
            string location = GetTableCellValue(9, 2);
            string department = GetTableCellValue(11, 2);
            string jobtitle = GetTableCellValue(12, 2);
            return (firtname + " " + lastname).Equals(submiter.FullName)
                && location.Contains(submiter.Location)
                && department.Equals(submiter.Department)
                && jobtitle.Equals(submiter.JobTitle);
        }
        #endregion
    }
}
