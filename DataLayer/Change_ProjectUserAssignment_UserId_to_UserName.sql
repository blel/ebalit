
-- Change Project User Assignments table
-- 20. Nov 2013
-- eb


alter table dbo.ProjectUserAssignments
drop FK_ProjectUserAssignment_aspnet_Users;


drop index   dbo.ProjectUserAssignments.IX_FK_ProjectUserAssignment_aspnet_Users;


alter table dbo.ProjectUserAssignments
drop column UserId;

alter table dbo.ProjectUserAssignments
add  MembershipUserName nvarchar(max);





