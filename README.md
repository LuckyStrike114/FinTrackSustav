# FinTrackSustav

**FinTrackSustav** je aplikacija za praćenje financijskih ciljeva i transakcija. Omogućuje korisnicima postavljanje financijskih ciljeva, praćenje napretka prema tim ciljevima te upravljanje transakcijama. Aplikacija je razvijena u C#-u koristeći WPF za grafičko sučelje i SQLite za pohranu podataka.

---

## Sadržaj
1. [Značajke](#značajke)
2. [Tehnologije](#tehnologije)
3. [Kako pokrenuti projekt](#kako-pokrenuti-projekt)
4. [Korištenje aplikacije](#korištenje-aplikacije)

---

## Značajke

- **Prijava i registracija korisnika**:
  - Korisnici se mogu prijaviti s postojećim računom ili registrirati novi.
  - Sigurnosna provjera podataka prilikom prijave i registracije.

- **Upravljanje financijskim ciljevima**:
  - Dodavanje novih financijskih ciljeva.
  - Praćenje napretka prema ciljevima (trenutni iznos vs. ciljni iznos).
  - Uređivanje i brisanje postojećih ciljeva.
  - Završetak cilja kada je postignut ciljni iznos.

- **Upravljanje transakcijama**:
  - Dodavanje novih transakcija s opisom, iznosom i datumom.
  - Prikaz svih transakcija korisnika.

- **Alokacija sredstava**:
  - Alokacija raspoloživih sredstava prema financijskim ciljevima.
  - Automatska provjera raspoloživih sredstava pri alokaciji.

- **Statistika i izvještaji**:
  - Prikaz ukupnog iznosa i raspoloživih sredstava.
  - Praćenje statusa financijskih ciljeva (Aktivan, Završen).

---

## Tehnologije

- **Programski jezik**: C#
- **Grafičko sučelje**: WPF (Windows Presentation Foundation)
- **Baza podataka**: SQLite
- **ORM**: Entity Framework Core
- **Razvojno okruženje**: Visual Studio

---

## Kako pokrenuti projekt

### Preduvjeti
- [Visual Studio](https://visualstudio.microsoft.com/) (preporučena verzija 2022 ili novija)
- [.NET SDK](https://dotnet.microsoft.com/download) (verzija 6.0 ili novija)

### Koraci za pokretanje
1. **Preuzimanje projekta**:
   - Klonirajte repozitorij na svoje računalo:
     ```bash
     git clone https://github.com/vaš-username/FinTrackSustav.git
     ```
   - Otvorite rješenje (`FinTrackSustav.sln`) u Visual Studiju.

2. **Instalacija paketa**:
   - Ako Visual Studio ne prepozna potrebne NuGet pakete, instalirajte ih putem NuGet Package Manager:
     - `Microsoft.EntityFrameworkCore.Sqlite`
     - `Microsoft.EntityFrameworkCore.Tools`

3. **Migracija baze podataka**:
   - Otvorite **Package Manager Console** u Visual Studiju.
   - Pokrenite migracije:
     ```bash
     Add-Migration InitialCreate
     Update-Database
     ```

4. **Pokretanje aplikacije**:
   - Pritisnite `F5` ili odaberite `Debug > Start Debugging` u Visual Studiju.

---

## Korištenje aplikacije

### Prijava i registracija
- Prilikom pokretanja aplikacije, korisnik se može prijaviti s postojećim računom ili registrirati novi.

### Upravljanje financijskim ciljevima
- Nakon prijave, korisnik može dodavati nove ciljeve, uređivati postojeće ili ih brisati.
- Ciljevi se mogu označiti kao "Završeni" kada je postignut ciljni iznos.

### Upravljanje transakcijama
- Korisnik može dodavati nove transakcije s opisom, iznosom i datumom.
- Sve transakcije su vidljive u pregledu transakcija.

### Alokacija sredstava
- Korisnik može alocirati raspoloživa sredstva prema financijskim ciljevima.
- Aplikacija automatski provjerava je li alocirani iznos manji od raspoloživih sredstava.

---

## Autori

- **Karlo Lauš** - [GitHub profil](https://github.com/LuckyStrike114)
- **Ivan Pribanić** - [GitHub profil](https://github.com/ivantf2)

---

Ako imate dodatnih pitanja ili prijedloga, slobodno otvorite [issue](https://github.com/vaš-username/FinTrackSustav/issues) na GitHubu.

---
