using Clean.UI.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.UI.Types
{
    public class BasePage : PageModel
    {

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        //private IConfiguration _configuration;
        //public BasePage(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        [BindProperty]
        public string PageID { get; set; }
        public List<SelectListItem> ListOfProcessesConnection;
        public List<SelectListItem> ListOfScreens;
        public List<SelectListItem> ListOfRoles;
        public List<SelectListItem> ListOfStatuses;
        public List<SelectListItem> ListOfCalculation;
        public List<SelectListItem> ListOfFiscalYears;
        public List<SelectListItem> ListOfHeritageJobs;
        public List<SelectListItem> ListOfEventReasons;
        public List<SelectListItem> ListOfLawyerTypes;
        public List<SelectListItem> ListOfAppReasons;
        public List<SelectListItem> ListOfCategoryTypes;
        public List<SelectListItem> ListOfMaritalStatus;
        public List<SelectListItem> ListOfGenders;
        public List<SelectListItem> ListOfLocations;
        public List<SelectListItem> ListOfBloodGroups;
        public List<SelectListItem> ListOfEthnicities;
        public List<SelectListItem> ListOfReligions;
        public List<SelectListItem> ListOfLanguages;
        public List<SelectListItem> ListOfJobStatus;
        public List<SelectListItem> ListOfResult = new List<SelectListItem>();
        public List<SelectListItem> ListOfRanks;
        public List<SelectListItem> ListOfBanks;
        public List<SelectListItem> ListOfOrganizationType;
        public List<SelectListItem> ListOfCertification;
        public List<SelectListItem> ListOfExperienceType;
        public List<SelectListItem> ListOfRelationShip;
        public List<SelectListItem> ListOfExpertise;
        public List<SelectListItem> ListOfSkillType;
        public List<SelectListItem> ListOfDistricts;
        public List<SelectListItem> ListOfEducationLevels;
        public List<SelectListItem> ListOfStatus;
        public List<SelectListItem> ListOfAssetType;
        public List<SelectListItem> ListOfReferenceType;
        public List<SelectListItem> ListOfDocumentTypes;
        public List<SelectListItem> ListOfDocumentTypesD;
        public List<SelectListItem> ListOfPublicationType;
        public List<SelectListItem> ListOfOrganization;
        public List<SelectListItem> ListOfOrgUnit = new List<SelectListItem>();
        public List<SelectListItem> ListOfSalaryType;
        public List<SelectListItem> ListOfReportTo;
        public List<SelectListItem> ListOfPlanType;
        public List<SelectListItem> ListOfPositionType;
        //public List<EducationLevel> ListOfOrganoGram = new List<EducationLevel>();
        public List<SelectListItem> ListOfMilitaryServiceType;
        public List<SelectListItem> ListOfEventType;
        public List<SelectListItem> ListOfPerson;
        public List<SelectListItem> ListOfPosition;
        public List<SelectListItem> ListOfProcesses = new List<SelectListItem>();
        public List<SelectListItem> ListOfPersianYears = new List<SelectListItem>();
        public List<SelectListItem> ListOfWorkAreas = new List<SelectListItem>();

        public List<SelectListItem> ListOfserviecetype;

        /// <summary>
        /// the ID of the Screen from the query string parameter
        /// </summary>
        public int RequestScreenID
        {
            get
            {
                return Convert.ToInt32(EncryptionHelper.Decrypt(Request.Query["p"]));
            }
        }


    }
}
