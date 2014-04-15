﻿Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports _3LAWMN12VW.ValidationRuleData

<TestClass()> Public Class TestApplicant

    <TestMethod()> Public Sub TestConstructor_Default()

        Dim tstPIObj As ProcessInfo = ProcessInfo.Fetch(Nothing, Nothing)
        Dim tstAppIdObj As Applicant = Applicant.Fetch("BUY", tstPIObj, Nothing, Nothing)
        Assert.AreEqual("BUY", tstAppIdObj.ApplicantType)
        Assert.AreEqual("", tstAppIdObj.ApplicantName.Family)
        Assert.AreEqual("", tstAppIdObj.ApplicantName.Given)
        Assert.AreEqual("", tstAppIdObj.ApplicantName.Middle)
        Assert.AreEqual("", tstAppIdObj.ApplicantName.BirthDate)
        Assert.AreEqual(-1, tstAppIdObj.ApplicantName.Age)
        Assert.AreEqual("", tstAppIdObj.ApplicantName.DriverLicense)
        Assert.AreEqual("", tstAppIdObj.ApplicantName.DriverLicenseExpiry)
        Assert.AreEqual("", tstAppIdObj.ApplicantName.IssuingState)
        Assert.AreEqual("", tstAppIdObj.ApplicantName.NationalID)
        Assert.AreEqual("", tstAppIdObj.ApplicantName.Suffix)
        Assert.AreEqual(Applicant.C_HOMEADDRESS, tstAppIdObj.HomeAddress.AddressType)
        Assert.AreEqual(True, tstAppIdObj.HomeAddress.SameAsHomeAddress)
        Assert.AreEqual("", tstAppIdObj.HomeAddress.Line1)
        Assert.AreEqual("", tstAppIdObj.HomeAddress.Line2)
        Assert.AreEqual("", tstAppIdObj.HomeAddress.City)
        Assert.AreEqual("", tstAppIdObj.HomeAddress.State)
        Assert.AreEqual("", tstAppIdObj.HomeAddress.Zip)
        Assert.AreEqual("", tstAppIdObj.HomeAddress.County)
        Assert.AreEqual(Applicant.C_BILLINGADDRESS, tstAppIdObj.BillingAddress.AddressType)
        Assert.AreEqual(True, tstAppIdObj.BillingAddress.SameAsHomeAddress)
        Assert.AreEqual("", tstAppIdObj.BillingAddress.Line1)
        Assert.AreEqual("", tstAppIdObj.BillingAddress.Line2)
        Assert.AreEqual("", tstAppIdObj.BillingAddress.City)
        Assert.AreEqual("", tstAppIdObj.BillingAddress.State)
        Assert.AreEqual("", tstAppIdObj.BillingAddress.Zip)
        Assert.AreEqual("", tstAppIdObj.BillingAddress.County)
        Assert.AreEqual(Applicant.C_GARAGEADDRESS, tstAppIdObj.GarageAddress.AddressType)
        Assert.AreEqual(True, tstAppIdObj.GarageAddress.SameAsHomeAddress)
        Assert.AreEqual("", tstAppIdObj.GarageAddress.Line1)
        Assert.AreEqual("", tstAppIdObj.GarageAddress.Line2)
        Assert.AreEqual("", tstAppIdObj.GarageAddress.City)
        Assert.AreEqual("", tstAppIdObj.GarageAddress.State)
        Assert.AreEqual("", tstAppIdObj.GarageAddress.Zip)
        Assert.AreEqual("", tstAppIdObj.GarageAddress.County)
    End Sub
    <TestMethod()> Public Sub TestConstructor()
        Dim pd As New Dictionary(Of String, Object)
        pd.Add("BUY_LASTNAME", "SBP")
        pd.Add("BUY_INITNAME", "N")
        pd.Add("BUY_FIRSTNAME", "SBP")
        pd.Add("BUY_BIRTHDATE", "01/05/2014")
        pd.Add("BUY_AGE", 99)
        pd.Add("BUY_SUFFIX", "I")
        pd.Add("BUY_SIN", "888-888-888")
        pd.Add("BUY_DLIC_NO", "999999999")
        pd.Add("BUY_DLIC_EXP", "01/06/2014")
        pd.Add("BUY_DLIC_STATE", "NM")
        pd.Add("BUY_GARAGE_SAMEASHOME", False)
        pd.Add("BUY_GARAGE_ADDRESS", "54321 EREHWYREVE TS.")
        pd.Add("BUY_GARAGE_CITY", "YRAGLAC")
        pd.Add("BUY_GARAGE_PROV", "BA")
        pd.Add("BUY_GARAGE_PCODE", "J2T 4S3")
        pd.Add("BUY_GARAGE_COUNTY", "REVOD")
        Dim d As New Dictionary(Of String, Object)
        d.Add("BUY_LASTNAME", "PBS")
        d.Add("BUY_INITNAME", "M")
        d.Add("BUY_FIRSTNAME", "PBS")
        d.Add("BUY_BIRTHDATE", "05/01/2014")
        d.Add("BUY_AGE", 10)
        d.Add("BUY_SIN", "999-999-999")
        d.Add("BUY_DLIC_NO", "1320941250974")
        d.Add("BUY_DLIC_EXP", "06/01/2014")
        d.Add("BUY_DLIC_STATE", "MN")
        d.Add("BUY_ADDRESS", "12345 EVERYWHERE ST.")
        d.Add("BUY_CITY", "Calgary")
        d.Add("BUY_PROV", "Alberta")
        d.Add("BUY_PCODE", "T2J 3S4")
        d.Add("BUY_COUNTY", "DOVER")
        Dim tstPIObj As ProcessInfo = ProcessInfo.Fetch(Nothing, Nothing)
        Dim tstAppIdObj As Applicant = Applicant.Fetch("BUY", tstPIObj, pd, d)
        Assert.AreEqual("BUY", tstAppIdObj.ApplicantType)
        Assert.AreEqual("PBS", tstAppIdObj.ApplicantName.Family)
        Assert.AreEqual("PBS", tstAppIdObj.ApplicantName.Given)
        Assert.AreEqual("M", tstAppIdObj.ApplicantName.Middle)
        Assert.AreEqual("I", tstAppIdObj.ApplicantName.Suffix)
        Assert.AreEqual("05/01/2014", tstAppIdObj.ApplicantName.BirthDate)
        Assert.AreEqual(10, tstAppIdObj.ApplicantName.Age)
        Assert.AreEqual("1320941250974", tstAppIdObj.ApplicantName.DriverLicense)
        Assert.AreEqual("06/01/2014", tstAppIdObj.ApplicantName.DriverLicenseExpiry)
        Assert.AreEqual("999-999-999", tstAppIdObj.ApplicantName.NationalID)
        Assert.AreEqual("MN", tstAppIdObj.ApplicantName.IssuingState)
        Assert.AreEqual(Applicant.C_HOMEADDRESS, tstAppIdObj.HomeAddress.AddressType)
        Assert.AreEqual(True, tstAppIdObj.HomeAddress.SameAsHomeAddress)
        Assert.AreEqual("12345 EVERYWHERE ST.", tstAppIdObj.HomeAddress.Line1, True)
        Assert.AreEqual("", tstAppIdObj.HomeAddress.Line2, True)
        Assert.AreEqual("Calgary", tstAppIdObj.HomeAddress.City, True)
        Assert.AreEqual("AB", tstAppIdObj.HomeAddress.State, True)
        Assert.AreEqual("T2J 3S4", tstAppIdObj.HomeAddress.Zip, True)
        Assert.AreEqual("DOVER", tstAppIdObj.HomeAddress.County, True)
        Assert.AreEqual(Applicant.C_GARAGEADDRESS, tstAppIdObj.GarageAddress.AddressType)
        Assert.AreEqual(False, tstAppIdObj.GarageAddress.SameAsHomeAddress)
        Assert.AreEqual("54321 EREHWYREVE TS.", tstAppIdObj.GarageAddress.Line1, True)
        Assert.AreEqual("", tstAppIdObj.GarageAddress.Line2, True)
        Assert.AreEqual("YRAGLAC", tstAppIdObj.GarageAddress.City, True)
        Assert.AreEqual("BA", tstAppIdObj.GarageAddress.State, True)
        Assert.AreEqual("J2T 4S3", tstAppIdObj.GarageAddress.Zip, True)
        Assert.AreEqual("REVOD", tstAppIdObj.GarageAddress.County, True)
    End Sub

End Class