## Verbindung zu Homeassistant
- Generierung des LangzeitToken

## API

### Beispieldatensätze

Dem Projekt liegen 2 Beispieldatensätze bei
1. Der Ordner **Set_0** enthält den Datensatz der während der gesamten Entwicklungszeit verwendet wurde
2. Der Ordner **Set_1** enthält den Datensatz der für die Fallstudie erstellt wurde
Beide Ordner enthalten auch die entsprechende Liste der **Entity_Includes** die in der **appsettings.json** eingefügt werden sollen.

### Setup
- Setzen der benötigten Werte in *appsettings.json*
    - URL der Homeassistant API
    - Bearer Token zur JWT-Authentifizierung mit der Homeassistant API
    - lokaler Pfad zu den Offline-Daten
    - Liste der herauszufilternden Entitäten
    ![appsettings.json](/API-appsettings.png){width=450}
- Setzen der DataService Implementierung per DependencyInjection für Online-/OfflineDaten
    - Mock_DataService für die konsistenten Offline Daten
    - HA_DataService für die direkte Verbindung mit der Homeassistant-API
![program.cs](/API-DI-DataService.png){width=450}

### Endpunkte
- Entities
    - `/api/entities` &rArr; `Task<IEnumerable<Entity>>`
    - `/api/entities/filtered` &rArr; `Task<IEnumerable<Entity>?>`
    - `/api/entities/list` &rArr; `Task<IEnumerable<string>>`
    - `/api/entites/states` &rArr; `Task<IEnumerable<AutomationStateEntry>>`
    - `/api/entites/{id}` &rArr; `Task<Entity>`
- Automations
    - `/api/automations` &rArr; `Task<IEnumerable<string>?>`
    - `/api/automations/states` &rArr; `Task<IEnumerable<AutomationStateEntry>?>`
    - `/api/automations/yaml/{name}` &rArr; `Task<YamlDefinition?>`
- Logbook
    - `/api/logbook` &rArr; `Task<IEnumerable<LogEntry>?>`
    - `/api/logbook/filtered` &rArr; `Task<IEnumerable<LogEntry>?>`

## Unity

### Setup
Unter dem Pfad **/Assets/StreamingAssets/appsettings.json** liegt die Konfigurationsdatei welche die *URL* der API enthält.
![appsettings.json](/Unity-appsettings.png){width=350}