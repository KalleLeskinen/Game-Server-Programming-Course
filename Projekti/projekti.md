# Projekti
Tein taustajärjestelmän jossa kuljettajat voivat tallentaa aikojaan eri ratojen ympäri 

## Rakenteesta

**Kuljettajia**
>**Kuskeihin** voi tallentaa **aikoja** ratojen ympäri

**Ratoja**
>**Radat** pitävät sisällään nopeimman mahdollisen ajan

**Aikoja**
>**Ajat** pitävät sisällään tiedon siitä millä **radalla** se on ajettu ja miten nopeaa, nämä tarkistetaan serverillä ennen tallentamista. Mikäli yritetään tallentaa mahdoton aika, palautetaan vain *virhe*

## HTTP Komennot



Luodaan kisaaja
**POST** /racers/create

    Body
    {
	    "Name":Esi Merkki
    }
Poistetaan kisaaja
**DELETE** /racers/delete/id

Haetaan kaikki kisaajat
**GET** /racers/getall

Tallennetaan kierros
**POST** /racers/GUID/SetNewLapTime/Track/TrackGUID

    Body
    {
	    "Time" : 70.403 //Aika sekunneissa
	}

Poistetaan kierros
**POST** /racers/GUID/DeleteLapTime/LapId/LapGUID

Haetaan kaikki kierrokset kisaajalta
**GET** /racers/GUID/GetAllLapTimes

Luodaan uusi rata
**POST** /tracks/CreateTrack

    Body
    {
	    "Name" : "Laguna Seca",
	    "FastestPossible" : 70.1 	//Nopein mahdollinen aika sekunneissa
	}

Päivitetään rataa
**PUT** /tracks/UpdateTrack/trackGUID

    Body
    {
	    "Name" : "Laguna Seca",
	    "FastestPossible" : 68.1 	//Nopein mahdollinen aika sekunneissa
	}

Poistetaan rata
**POST** /tracks/DeleteTack/trackGUID
