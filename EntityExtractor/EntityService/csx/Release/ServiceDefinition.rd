<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="EntityService" generation="1" functional="0" release="0" Id="abf82147-0f9e-404c-9ce7-236aa8913a35" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="EntityServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="EntityServiceWorkerRole:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/EntityService/EntityServiceGroup/LB:EntityServiceWorkerRole:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="EntityServiceWorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/EntityService/EntityServiceGroup/MapEntityServiceWorkerRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:EntityServiceWorkerRole:Endpoint1">
          <toPorts>
            <inPortMoniker name="/EntityService/EntityServiceGroup/EntityServiceWorkerRole/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapEntityServiceWorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/EntityService/EntityServiceGroup/EntityServiceWorkerRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="EntityServiceWorkerRole" generation="1" functional="0" release="0" software="D:\Hackathon\project\icehackathon\EntityExtractor\EntityService\csx\Release\roles\EntityServiceWorkerRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;EntityServiceWorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;EntityServiceWorkerRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/EntityService/EntityServiceGroup/EntityServiceWorkerRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/EntityService/EntityServiceGroup/EntityServiceWorkerRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/EntityService/EntityServiceGroup/EntityServiceWorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="EntityServiceWorkerRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="EntityServiceWorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="EntityServiceWorkerRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="2e59f6d1-8b28-436f-b22b-a83e8d5105cd" ref="Microsoft.RedDog.Contract\ServiceContract\EntityServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="b98ac5f6-f79e-465f-99f2-665fc567fe96" ref="Microsoft.RedDog.Contract\Interface\EntityServiceWorkerRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/EntityService/EntityServiceGroup/EntityServiceWorkerRole:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>