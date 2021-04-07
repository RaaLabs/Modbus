﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.7.0.0
//      SpecFlow Generator Version:3.7.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace RaaLabs.Edge.Connectors.Modbus.Specs.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.7.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class ModbusBridgeFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "ModbusBridge.feature"
#line hidden
        
        public virtual Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
        {
            get
            {
                return this._testContext;
            }
            set
            {
                this._testContext = value;
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "ModbusBridge", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "ModbusBridge")))
            {
                global::RaaLabs.Edge.Connectors.Modbus.Specs.Features.ModbusBridgeFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>(_testContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 3
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Unit",
                        "StartingAddress",
                        "DataType",
                        "FunctionCode",
                        "Size",
                        "Content"});
            table3.AddRow(new string[] {
                        "1",
                        "1",
                        "4",
                        "1",
                        "1",
                        "ABCD"});
            table3.AddRow(new string[] {
                        "1",
                        "2",
                        "4",
                        "1",
                        "1",
                        "1234"});
            table3.AddRow(new string[] {
                        "1",
                        "3",
                        "2",
                        "1",
                        "1",
                        "2345 6789"});
            table3.AddRow(new string[] {
                        "1",
                        "4",
                        "2",
                        "1",
                        "2",
                        "5678 1234  9876 5432"});
#line 4
 testRunner.Given("a Modbus Register with the following contents", ((string)(null)), table3, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Unit",
                        "StartingAddress",
                        "DataType",
                        "FunctionCode",
                        "Size"});
            table4.AddRow(new string[] {
                        "1",
                        "1",
                        "4",
                        "1",
                        "1"});
            table4.AddRow(new string[] {
                        "1",
                        "2",
                        "4",
                        "1",
                        "1"});
            table4.AddRow(new string[] {
                        "1",
                        "3",
                        "2",
                        "1",
                        "1"});
            table4.AddRow(new string[] {
                        "1",
                        "4",
                        "2",
                        "1",
                        "2"});
#line 10
 testRunner.And("a register configuration with the following values", ((string)(null)), table4, "And ");
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Connecting to Modbus from ModbusBridge")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ModbusBridge")]
        public virtual void ConnectingToModbusFromModbusBridge()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Connecting to Modbus from ModbusBridge", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 17
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "Ip",
                            "Port,",
                            "Endianness",
                            "Protocol",
                            "UseASCII",
                            "Interval",
                            "ReadTimeout"});
                table5.AddRow(new string[] {
                            "\"127.0.0.1\"",
                            "502",
                            "1",
                            "1",
                            "true",
                            "-1",
                            "1000"});
#line 18
 testRunner.Given("a connector configuration with the following values", ((string)(null)), table5, "Given ");
#line hidden
#line 21
 testRunner.When("establishing connection to modbus", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                            "Unit",
                            "StartingAddress",
                            "FunctionCode",
                            "Size",
                            "Content"});
                table6.AddRow(new string[] {
                            "1",
                            "1",
                            "1",
                            "1",
                            "ABCD"});
                table6.AddRow(new string[] {
                            "1",
                            "2",
                            "1",
                            "1",
                            "1234"});
                table6.AddRow(new string[] {
                            "1",
                            "3",
                            "1",
                            "1",
                            "2345 6789"});
                table6.AddRow(new string[] {
                            "1",
                            "4",
                            "1",
                            "2",
                            "5678 1234  9876 5432"});
#line 22
 testRunner.Then("the following ModbusRegisterReceived events should be emitted", ((string)(null)), table6, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
