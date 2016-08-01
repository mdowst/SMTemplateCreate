using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EnterpriseManagement;
using Microsoft.EnterpriseManagement.Common;
using Microsoft.EnterpriseManagement.Configuration;
using Microsoft.EnterpriseManagement.ConnectorFramework;


namespace Create_WI_from_Template
{
    public class CreateWI
    {

        public Guid TemplateID;
        public EnterpriseManagementGroup emg = null;

        public string WorkItem()
        {

            //Create Work Item
            ManagementPackObjectTemplate mpt = emg.Templates.GetObjectTemplate(TemplateID);
            EnterpriseManagementObjectProjection emop = new EnterpriseManagementObjectProjection(emg, mpt);

            // Determine prefix and status based on class
            string prefix = null;
            Guid status = default(Guid); ;
            Guid WIType = emop.Object.LeastDerivedNonAbstractManagementPackClassId;
            if (WIType == Constants.SRClass)
            {
                prefix = WIPrefix(Constants.SRSettings, Constants.SRPrefix);
                status = Constants.SRStatusEnum;
            }
            else if (WIType == Constants.CRClass)
            {
                prefix = WIPrefix(Constants.CRSettings, Constants.CRPrefix);
                status = Constants.CRStatusEnum;
            }
            else if (WIType == Constants.PRClass)
            {
                prefix = WIPrefix(Constants.PRSettings, Constants.PRPrefix);
                status = Constants.PRStatusEnum;
            }
            else if (WIType == Constants.IRClass)
            {
                prefix = WIPrefix(Constants.IRSettings, Constants.IRPrefix);
                status = Constants.IRStatusEnum;
            }
            else if (WIType == Constants.RRClass)
            {
                prefix = WIPrefix(Constants.RRSettings, Constants.RRPrefix);
                status = Constants.RRStatusEnum;
            }

            // Add prefix to ID
            string strSRID = prefix + emop.Object[null, "Id"].ToString();
            emop.Object[null, "Id"].Value = strSRID;

            // Set the status
            emop.Object[null, "Status"].Value = emg.EntityTypes.GetEnumeration(status);

            // Set the Created Date and Display Name
            emop.Object[null, "CreatedDate"].Value = DateTime.Now.ToUniversalTime();
            emop.Object[null, "DisplayName"].Value = (string)emop.Object[null, "Id"].Value + " - " + (string)emop.Object[null, "Title"].Value;

            // Set the Created By User
            string getByTokenCriteria = @"<Criteria  xmlns='http://Microsoft.EnterpriseManagement.Core.Criteria/'>
                                          <Expression>
                                            <SimpleExpression>
                                              <ValueExpressionLeft>
                                                <GenericProperty>Id</GenericProperty>
                                              </ValueExpressionLeft>
                                              <Operator>Equal</Operator>
                                              <ValueExpressionRight>
                                                <Token>[me]</Token>
                                              </ValueExpressionRight>
                                            </SimpleExpression>
                                          </Expression>
                                        </Criteria>";

            ManagementPackRelationship CreatedByUserRel = emg.EntityTypes.GetRelationshipClass(Constants.CreatedByUserRelId);
            ManagementPackClass ADUserBaseClass = emg.EntityTypes.GetClass(Constants.ADUserBaseClassId);
            EnterpriseManagementObjectCriteria GetUserByDomainAndName = new EnterpriseManagementObjectCriteria(getByTokenCriteria, ADUserBaseClass, emg);
            EnterpriseManagementObject objCurrentUser = emg.EntityObjects.GetObjectReader<EnterpriseManagementObject>(GetUserByDomainAndName, ObjectQueryOptions.Default).FirstOrDefault();
            if (objCurrentUser != null)
            {
                emop.Add(objCurrentUser, CreatedByUserRel.Target);
            }

            // Set the prefixes for the child activities
            SetIdPrefixToChildActivity(emop);

            // Save the work item and return the Guid
            emop.Overwrite();
            var strWIGuid = emop.Object.Id.ToString();

            return strWIGuid;
        }

        public string WIPrefix(Guid SettingClass, string SettingPrefix)
        {
            ManagementPackClass settingMPC = emg.EntityTypes.GetClass(SettingClass);
            EnterpriseManagementObject emo = emg.EntityObjects.GetObject<EnterpriseManagementObject>(settingMPC.Id, ObjectQueryOptions.Default);
            string prefix = emo[settingMPC, SettingPrefix].Value.ToString();

            return prefix;
        }

        void SetIdPrefix(EnterpriseManagementObject ActEmo)
        {
            // check all prefixes for activity
            //EnterpriseManagementObject ActEmo = objCurComponent.Object;
            EnterpriseManagementObject objSettings = emg.EntityObjects.GetObject<EnterpriseManagementObject>(Constants.ACSettings, ObjectQueryOptions.Default);
            // set default prefix
            string Actprefix = objSettings[null, Constants.ACPrefix].Value.ToString();
            // check more specified activities
            // Get Activity Types
            ManagementPackClass MAc = emg.EntityTypes.GetClass(Constants.MAClass);
            ManagementPackClass RBc = emg.EntityTypes.GetClass(Constants.RBClass);
            ManagementPackClass RAc = emg.EntityTypes.GetClass(Constants.RAClass);
            ManagementPackClass PAc = emg.EntityTypes.GetClass(Constants.PAClass);
            ManagementPackClass SAc = emg.EntityTypes.GetClass(Constants.SAClass);
            ManagementPackClass DAc = emg.EntityTypes.GetClass(Constants.DAClass);
            if (ActEmo.IsInstanceOf(MAc))
            {
                Actprefix = objSettings[null, Constants.MAPrefix].Value.ToString();
            }
            else if (ActEmo.IsInstanceOf(RBc))
            {
                Actprefix = objSettings[null, Constants.RBPrefix].Value.ToString();
            }
            else if (ActEmo.IsInstanceOf(RAc))
            {
                Actprefix = objSettings[null, Constants.RAPrefix].Value.ToString();
            }
            else if (ActEmo.IsInstanceOf(PAc))
            {
                Actprefix = objSettings[null, Constants.PAPrefix].Value.ToString();
            }
            else if (ActEmo.IsInstanceOf(SAc))
            {
                Actprefix = objSettings[null, Constants.SAPrefix].Value.ToString();
            }
            else if (ActEmo.IsInstanceOf(DAc))
            {
                Actprefix = objSettings[null, Constants.DAPrefix].Value.ToString();
            }
            
            // Set the prefix
            string strActID = Actprefix + ActEmo[null, "Id"].Value.ToString();
            ActEmo[null, "Id"].Value = strActID;
            
            // set display name
            ActEmo[null, "DisplayName"].Value = (string)ActEmo[null, "Id"].Value + " - " + (string)ActEmo[null, "Title"].Value;

        }

        void SetIdPrefixToChildActivity(IComposableProjection objProjectionComponent)
        {
            ManagementPackRelationship relContainsActivity = emg.EntityTypes.GetRelationshipClass(Constants.WIContainsACRelId);
            foreach (IComposableProjection objCurComponent in objProjectionComponent[relContainsActivity.Target])
            {
                SetIdPrefix(objCurComponent.Object);
                // call recursive
                SetIdPrefixToChildActivity(objCurComponent);
            }
        }

        public static EnterpriseManagementGroup GetManagementGroupConnection(string strComputerName)
        {
            EnterpriseManagementGroup emg = new EnterpriseManagementGroup(strComputerName);
            return emg;
        }

    }
}
