using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace sunyvpp
{
    public partial class FrmConfig : UIForm
    {
        private OptionSetting settingOption;
        public static FrmConfig frmset = null;
        public FrmConfig()
        {
            InitializeComponent();
            //加载OptionSetting参数
            //XMLMethod.ReadNodeAndInnerText(ConfigureFilePath.Path_Config_Setting_Option, "OptionSetting", out Globals.SettingOption);
            if (File.Exists(ConfigureFilePath.projectItemPath + "\\" + "SysParam.json"))
            {
                Globals.SettingOption = (OptionSetting)JsonHelper.Instance.JsonFileToObject(ConfigureFilePath.projectItemPath + "\\" + "SysParam.json", Globals.SettingOption);
            }
            //settingOption = Globals.Copy(Globals.SettingOption);
            propertyGrid1.SelectedObject = Globals.SettingOption;
        }
        public static FrmConfig GetInstance()
        {
            if (frmset == null || (frmset != null && frmset.IsDisposed))
            {
                frmset = new FrmConfig();
            }
            return frmset;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            JsonHelper.Instance.ObjectToJsonFile(ConfigureFilePath.projectItemPath + "\\" + "SysParam.json", Globals.SettingOption);
            if (File.Exists(ConfigureFilePath.projectItemPath + "\\" + "SysParam.json"))
            {
                Globals.SettingOption = (OptionSetting)JsonHelper.Instance.JsonFileToObject(ConfigureFilePath.projectItemPath + "\\" + "SysParam.json", Globals.SettingOption);
            }
            else
            {
                Globals.LogRecord("【系统参数加载失败！】");
                ShowErrorDialog("系统参数加载失败");
                return;
                
            }
            //settingOption = Globals.Copy(Globals.SettingOption);
            propertyGrid1.SelectedObject = Globals.SettingOption;
            Globals.LogRecord("【保存成功！】");
            //MessageBox.Show("参数修改成功！");
            ShowInfoDialog("参数保存", "参数保存成功！", UIStyle.Green);
            //ShowSuccessDialog("参数保存成功！");
            #region MyRegion
            //if (MessageBox.Show("是否修改参数？", "参数修改", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            //{
            //    //修改保存OptionSetting参数
            //    if (XMLMethod.UpdateInnerText(ConfigureFilePath.Path_Config_Setting_Option, "OptionSetting", Globals.SettingOption) == 0)
            //    {
            //        Type type = Globals.SettingOption.GetType();
            //        var properties = type.GetProperties();
            //        //Dictionary<string, string[]> dic = new Dictionary<string, string[]>();
            //        foreach (PropertyInfo property in properties)
            //        {
            //            var temp1 = property.GetValue(Globals.SettingOption);
            //            var temp2 = property.GetValue(settingOption);
            //            if (!temp1.Equals(temp2))
            //            {
            //                property.SetValue(Globals.SettingOption, temp2);
            //                WriteLog(property.Name, "从" + temp1.ToString() + "修改为" + temp2.ToString());
            //                MessageBox.Show(property.Name + "从" + temp1.ToString() + "修改为" + temp2.ToString());
            //                //string[] strArray = { temp1.ToString(), temp2.ToString() };
            //                //dic.Add(property.Name, strArray);
            //                Globals.LogRecord("保存成功！");
            //            }
            //        }
            //        //foreach (var item in dic)
            //        //{
            //        //    WriteLog(item.Key, "从" + item.Value[0] + "修改为" + item.Value[1]);
            //        //}
            //        XMLMethod.UpdateInnerText(ConfigureFilePath.Path_Config_Setting_Option, "OptionSetting", Globals.SettingOption);
            //        MessageBox.Show("参数修改成功！");
            //    }
            //}
            #endregion

        }
    }
}
