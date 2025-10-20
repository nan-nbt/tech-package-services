# Tech Package Services Documentation

## Overview

Tech Package Services is a C# static class that provides integration with CPS (Customs Processing System) APIs for handling ASN (Advance Shipment Notice) data transmission and BC (Bill of Collection) data retrieval used in local network on some company and developed using .NET Core framework.

## Methods

### SendToCPS
Sends ASN data to CPS for customs processing.

```csharp
public static string SendToCPS(string factNo, string asnNo, string trfStatus)
```

**Parameters:**
- `factNo`: Factory number
- `asnNo`: ASN number
- `trfStatus`: Transfer status (C: Create, D: Delete)

**Returns:** "success" or error message

### GetBC
Retrieves BC data from CPS and synchronizes to client system.

```csharp
public static string GetBC(string compNo, string factNo, string asnNo)
```

**Parameters:**
- `compNo`: Company number
- `factNo`: Factory number
- `asnNo`: ASN number

**Returns:** "success" or error message

## Endpoint Invocation Examples

### Environments
- `DEV`: Development area
- `QAS`: Test area
- `PGS`: Official area for PGS
- `PGD`: Official area for PGD
- `PCA`: Official area for PCA
- `PYN`: Official area for PYN

### Usage #1
`METHOD` `Host`/RIAService/AjaxServiceNew.ashx?JsonService={service:"PCaG.ESB.TechPackage.ESBService.`End Point Method`$`Environment`",params:`[]`}

### Usage #2
`METHOD` `Host`/RIAService/API/PCaG/ESB/TechPackage/ESBService/`End Point Method`$`Environment`

Authorization: `AuthType` `AuthToken`


`Body`

**Description**
- `METHOD`: GET/POST
- `Host`: Server Host (i.e: localhost:5000, esbac-qas.pci.co.id)
- `End Point Method`: SendToCPS/GetBC
- `Environment`: DEV/QAS/PGS/PGD/PCA/PYN
- `[]`: `[Parameters]` used in End Point Method
- `AuthType`: Basic/Bearer
- `AuthToken`: Authorization Token
- `Body`: RAW Array ([])

### SendToCPS - Local Development (Usage #1)
```http
POST http://localhost:5000/RIAService/AjaxServiceNew.ashx?JsonService={service:"PCaG.ESB.TechPackage.ESBService.SendToCPS$DEV",params:["F001","ASN001","C"]}
```

### SendToCPS - QAS Environment (Usage #2)
```http
POST https://tech-package-qas.com/RIAService/API/PCaG/ESB/TechPackage/ESBService/SendToCPS$QAS
Authorization: Basic authToken5010=

["F001","ASN002","C"]
```

### SendToCPS - PRD Environment (Usage #2)
```http
POST https://tech-package-prd.com/RIAService/API/PCaG/ESB/TechPackage/ESBService/SendToCPS$PGS
Authorization: Basic authToken5010=

["F001","ASN002","C"]
```