<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="EntityService" generation="1" functional="0" release="0" Id="2213020a-a608-4f9b-aa2b-8bc465ecf677" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
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
          <role name="EntityServiceWorkerRole" generation="1" functional="0" release="0" software="D:\Hackathon\project\icehackathon\EntityExtractor\EntityService\csx\Debug\roles\EntityServiceWorkerRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
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
    <implementation Id="0ec3e697-e218-4764-92d0-a531e8987150" ref="Microsoft.RedDog.Contract\ServiceContract\EntityServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="3bfe338c-7b83-451a-806f-bea008d642dc" ref="Microsoft.RedDog.Contract\Interface\EntityServiceWorkerRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/EntityService/EntityServiceGroup/EntityServiceWorkerRole:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>