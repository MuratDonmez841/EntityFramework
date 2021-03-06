// <copyright file="ServicesTest.cs">Copyright ©  2019</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using deneme.Services;

namespace deneme.Services.Tests
{
    [TestClass]
    [PexClass(typeof(global::deneme.Services.Services))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ServicesTest
    {

        [PexMethod(MaxBranches = 20000)]
        public void getViews([PexAssumeUnderTest]global::deneme.Services.Services target, int id)
        {
            target.getViews(id);
            // TODO: ServicesTest.getViews(Services, Int32) metodu metoduna onay deyimleri ekle
        }
    }
}
