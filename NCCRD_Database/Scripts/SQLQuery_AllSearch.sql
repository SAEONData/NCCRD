SELECT 
	pd.ProjectDetailsId,
	ProjectDetailsIdentifier,
	LeadAgent,
	ProjectTitle,
	HostPartner,
	FundingOrganisation,
	FundingPartner,
	HostOrganisation,
	pd.[Status],
	'Adaptation' as  'Intervention', 
	'Project' as 'ProjectType',
	prvnce.ProvinceID  as Province,
	twn.TownID  as Town,
	lm.LocalMunicipalityID as 'Localmunicipality', 
	0 as 'CO2e',
	mdm.MetroID as Metro ,
	pd.ValidationStatus as 'ValidationStatus' ,
	' ' as 'ResearchType' ,
	pd.StartYear,
	pd.EndYear,
	pd.EstimatedBudget,
	pd.AlternativeContact,
	pd.AlternativeContactEmail,
	pd.Link
	,convert(nvarchar(4000),pd.Description) as 'Description',
	pd.FUNDINGSTATUS,
	pd.BudgetLower,
	pd.BudgetUpper,
	0 as 'HostSector',
	0 as 'HostSubSector',
	0 as 'HostSubSubSector'
from 
	tb_erm_project_details pd
left join 
	(
		select 
			* 
		from 
			tb_erm_picklist_value 
		where 
			listid = (
						select 
							listid 
						from 
							tb_erm_picklist 
						where 
							ListName = 'Type of Project'
					)
	) TypeProject 
	on pd.TypeOfProject = TypeProject.ItemNum 
left join 
	(
		select 
			* 
		from 
			tb_erm_picklist_value 
		where listid = (
							select 
								listid 
							from 
								tb_erm_picklist 
							where ListName = 'Status'
						)
	) [Status] 
	on pd.Status = Status.ItemNum 
left join 
	(
		select 
			* 
		from 
			tb_erm_picklist_value 
		where 
			listid = (
						select 
							listid 
						from 
							tb_erm_picklist 
						where 
							ListName = 'Adaptation Host Sector'
					)
	) HostSector 
	on pd.HostSector = HostSector.ItemNum 
left join 
	tb_erm_Project_Adaptation_Data pad 
	on pd.ProjectDetailsId = pad.ProjectDetailsId
inner join 
	tb_erm_Province prvnce 
	on pad.Province = prvnce.ProvinceID 
left join 
	tb_erm_Metro_DistrictMunicipality mdm 
	on pad.Municipality = mdm.MetroID
left join 
	tb_erm_Local_Municipalities lm 
	on  pad.LocalMunicipality =lm.LocalMunicipalityID
left join 
	tb_erm_Town twn 
	on pad.city=twn.TownID where pd.TypeOfIntervention='2' 
	and pd.TypeOfProject='1'
	 
UNION
 
SELECT 
	pd.ProjectDetailsId,
	ProjectDetailsIdentifier,
	LeadAgent,
	ProjectTitle,
	HostPartner,
	FundingOrganisation,
	FundingPartner,
	HostOrganisation,
	pd.[Status],
	'Mitigation' as  'Intervention',
	'Project' as 'ProjectType',
	prvnce.ProvinceID  as Province,
	twn.TownID  as Town,
	lm.LocalMunicipalityID as 'Localmunicipality',
	(
		select 
			sum(CO2) + sum(CH4_CO2e) + sum(N2O_CO2e) + sum(HFC_CO2e) + sum(PFC_CO2e)+ sum(SF6_CO2e) + sum(BioWaste_CO2e) + 
			sum(Geothermal_CO2e) + sum(Hydro_CO2e) + sum(Solar_CO2e) + sum(Tidal_CO2e) + sum(Wind_CO2e) + sum(FossilFuelElecRed_CO2e)
		from 
			dbo.tb_erm_Mitigation_Emissions_Data  med1 
		where 
			med1.ProjectLocationDataId = pld.ProjectLocationDataId
	) as 'CO2e',
	mdm.MetroID as Metro ,
	pd.ValidationStatus as 'ValidationStatus',
	' ' as 'ResearchType' ,
	pd.StartYear,
	pd.EndYear,
	pd.EstimatedBudget,
	pd.AlternativeContact,
	pd.AlternativeContactEmail,
	pd.Link
	,convert(nvarchar(4000),pd.Description) as 'Description',
	pd.FUNDINGSTATUS,
	pd.BudgetLower,
	pd.BudgetUpper
	,HostSector.itemnum as 'HostSector',
	tmms.MainSubSectorId as 'HostSubSector',
	tmss.SubSectorId as 'HostSubSubSector'
from 
	tb_erm_project_details pd
left join 
	(
		select 
			* 
		from 
			tb_erm_picklist_value 
		where 
			listid = (
						select 
							listid 
						from 
							tb_erm_picklist 
						where ListName = 'Status'
					)
	) [Status] 
	on pd.Status = Status.ItemNum   
left join 
	tb_erm_Project_Location_Data pld 
	on pd.ProjectDetailsId = pld.ProjectDetailsId
inner join 
	tb_erm_Province prvnce 
	on pld.Province = prvnce.ProvinceID 
left join 
	tb_erm_Metro_DistrictMunicipality mdm 
	on pld.Metro = mdm.MetroID
left join 
	tb_erm_Local_Municipalities lm 
	on  pld.LocalMunicipality =lm.LocalMunicipalityID
left join 
	tb_erm_Town twn 
	on pld.Town=twn.TownID
left join 
	tb_erm_mitigation_details mt 
	on pd.ProjectDetailsId = mt.ProjectDetailsId 
left join 
	(
		select 
			* 
		from 
			tb_erm_picklist_value 
		where listid = (
							select 
								listid 
							from 
								tb_erm_picklist 
							where 
								ListName = 'Host Sector'
						)
	) HostSector 
	on mt.HostSector = HostSector.ItemNum
left join 
	tb_erm_Mitigation_MainSubSector tmms 
	on tmms.MainSubSectorId =mt.HostMainSubSector
left join 
	tb_erm_Mitigation_SubSector tmss 
	on tmss.SubSectorId =mt.HostSubSector
left join 
	(
		select 
			* 
		from 
			tb_erm_picklist_value 
		where listid = (
							select 
								listid 
							from 
								tb_erm_picklist 
							where ListName = 'Type of Project'
						)
	) TypeProject 
	on mt.ProjectType = TypeProject.ItemNum
where 
	pd.TypeOfIntervention='1' 
	and 
	pd.TypeOfProject='1' 

UNION 

SELECT 
	pd.ProjectDetailsId,
	ProjectDetailsIdentifier,
	LeadAgent,
	ProjectTitle,
	HostPartner,
	FundingOrganisation,
	FundingPartner,
	HostOrganisation,
	pd.[Status], 
	case 
		pd.TypeOfIntervention 
		when 1 then 'Mitigation' 
		else 'Adaptation ' end  
		as 'Intervention', 
	'Research' as 'ProjectType', 
	prvnce.ProvinceID  as Province,
	twn.TownID  as Town,
	lm.LocalMunicipalityID as 'Localmunicipality',
	0 as 'CO2e',
	mdm.MetroID as Metro ,
	pd.ValidationStatus as 'ValidationStatus',
	ResearchType.ItemDisplay as 'ResearchType',
	pd.StartYear,
	pd.EndYear,
	pd.EstimatedBudget,
	pd.AlternativeContact,
	pd.AlternativeContactEmail,
	pd.Link
	,convert(nvarchar(4000),pd.Description) as 'Description',
	pd.FUNDINGSTATUS,
	pd.BudgetLower,
	pd.BudgetUpper,
	0 as 'HostSector',
	0 as 'HostSubSector',
	0 as 'HostSubSubSector'
from 
	tb_erm_project_details pd
left join 
	(
		select 
			* 
		from 
			tb_erm_picklist_value 
		where listid = (
							select 
								listid 
							from 
								tb_erm_picklist 
							where 
								ListName = 'Status'
						)
	) [Status] 
	on pd.Status = Status.ItemNum   
left join 
	tb_erm_Project_Research_Data rd 
	on pd.ProjectDetailsId = rd.ProjectDetailsId 
left join 
	(
		select 
			* 
		from 
			tb_erm_picklist_value 
		where listid = (
							select 
								listid 
							from 
								tb_erm_picklist 
							where 
								ListName = 'Type of research'
						)
	) ResearchType 
	on rd.TypeOfResearch = ResearchType.ItemNum
inner join 
	tb_erm_Province prvnce 
	on rd.Province = prvnce.ProvinceID 
left join 
	tb_erm_Metro_DistrictMunicipality mdm 
	on rd.Municipality = mdm.MetroID
left join 
	tb_erm_Local_Municipalities lm 
	on  rd.LocalMunicipality =lm.LocalMunicipalityID
left join 
	tb_erm_Town twn 
	on rd.City=twn.TownID
left join 
	(
		select 
			* 
		from 
			tb_erm_picklist_value 
		where listid = (
							select 
								listid 
							from
								tb_erm_picklist 
							where ListName = 'Target audience'
						)
	) tarAudience 
	on rd.TargetAudience = tarAudience.ItemNum
where 
	pd.TypeOfProject='2'  



