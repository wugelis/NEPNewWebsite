using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using YuanNewWebsite.Areas.YuanAdmin.Models;
using YuanNewWebsite.Tests.MyFakes;
using YuanNewWebsite.VO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace YuanNewWebsite.Tests
{
    [TestClass]
    public class Test_ErNetAdminModels
    {
        [TestMethod]
        public void Test_CheckERNetResult()
        {
            //Arrange
            Mock<IErNetAdminModel> target = new Mock<IErNetAdminModel>();
            List<MyTestPostedFileBase> files = new List<MyTestPostedFileBase>(new MyTestPostedFileBase[] { null, null});
            IErNetAdminModel erNetAdminModel = ErNetAdminModelFactory.Create();
            AnnouncementUploadVO expected = new AnnouncementUploadVO() { Message = "請選擇任一檔案！" };
            AnnouncementUploadVO actual = null;

            target
                .Setup(c => c.CheckERNetResult(files))
                .Returns(erNetAdminModel.CheckERNetResult(files));

            //Act
            actual = target.Object.CheckERNetResult(files);

            //Assert
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod]
        public void Test_CheckERNetResult_Runtime()
        {
            //Arrange
            IErNetAdminModel target = ErNetAdminModelFactory.Create();
            List<MyTestPostedFileBase> files = new List<MyTestPostedFileBase>(
                new MyTestPostedFileBase[] {
                    new MyTestPostedFileBase(new MemoryStream(), "multipart/form-data", "index.htm") { },
                    new MyTestPostedFileBase(new MemoryStream(), "multipart/form-data", "ernet-main.htm") { }
            });
            AnnouncementUploadVO expected = new AnnouncementUploadVO()
            {
                Message = "最新公告檔案檔案名稱為『ernet-new.htm』，歷史公告檔案名稱為『ernet-main.htm』！"
            };
            AnnouncementUploadVO actual = null;

            //Act
            actual = target.CheckERNetResult(files.Select(c => (HttpPostedFileBase) c));

            //Assert
            Assert.IsTrue(actual != null);
            Assert.AreEqual(expected.Message, actual.Message);
        }
    }
}
