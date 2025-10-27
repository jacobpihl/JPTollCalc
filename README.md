_Filen är AI-genererad och korrekturläst av Jacob Pihl_

# JPTollCalc - Svensk Trängselskatt Kalkylator

Ett .NET 9-projekt för beräkning av svensk trängselskatt i Göteborg. Systemet hanterar olika fordonstyper och tidsbaserade avgifter enligt gällande regler.

## Projektstruktur

- **JPTollCalc.Contracts** - Kontrakt och gränssnitt för fordonstyper
- **JPTollCalc.Business** - Affärslogik för avgiftsberäkning
- **JPTollCalc.Console** - Enkel konsol-applikation för demonstration
- **JPTollCalc.Business.Tests** - Enhetstester med NUnit

## Använda Kvalitetskoncept

### 1. Clean Architecture & Separation of Concerns

- **Contracts-projektet** definierar gränssnitt och datastrukturer som är oberoende av implementation
- **Business-projektet** innehåller ren affärslogik utan externa beroenden
- **Test-projektet** innehåller tester
- **Fördelar**: Modulär kod, enkel testning, låg koppling mellan komponenter

### 2. Enum för Type Safety

```csharp
public enum VehicleType
{
    Motorbike = 0, Tractor = 1, Emergency = 2, // ...
}
```

- Starkt typade konstanter istället för magiska tal eller strängar
- **Fördelar**: Kompileringstidskontroll, IntelliSense-stöd, mindre risk för buggar

### 3. Unit Testing

- **'En del' tester** som täcker olika scenarion
- **Parametriserade testmetoder** för att testa samma logik på olika fordonstyper
- **Tydliga testnamn** som beskriver exakt vad som testas
- **Fördelar**: Säkerhet vid refactoring, dokumentation av förväntad behavior, regression protection

### 4. Immutable Data Structures

- `VehicleType` som readonly property via interface
- DateTime-arrays som input istället för muterbara collections
- **Fördelar**: Thread safety, förutsägbar behavior, mindre bieffekter

### 5. Single Responsibility Principle (SRP)

- `TollCalculator` har endast ansvar för avgiftsberäkning
- Separata metoder för olika aspekter (datum-kontroll, fordons-kontroll, avgifts-logik)
- **Fördelar**: Lättare att förstå, testa och underhålla

### 6. External Library Integration

- Använder `PublicHoliday` NuGet-paket för helgdagar
- **Fördelar**: Återanvändning av befintliga lösningar, mindre underhåll av domän-specifik logik

## Framtida Förbättringar

### 1. Konfiguration och Flexibilitet

- **Externalisera tariffer**: Flytta ut avgiftssatser och tider till konfigurationsfiler eller databas
- **Admin-gränssnitt**: Möjlighet att ändra tariffer utan kodändringar

### 2. Arkitektur & Design Patterns

- **Dependency Injection**: Implementera IoC container för bättre testbarhet
- **Repository Pattern**: För persisteringslager

### 3. Felhantering & Robusthet

- **Input Validation**: Validering av inkommande data
- **Logging**: Strukturerad loggning för debugging och monitoring
- **Circuit Breaker**: För externa tjänsteanrop (helgdagar)

### 4. Testning

- **Integration Tests**: Testa hela flöden

---

_Skapad: Oktober 2025_
