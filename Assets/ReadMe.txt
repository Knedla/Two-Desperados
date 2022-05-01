ovo nije kako bi ja uradio projkat definitivno, nego vise nabacivanje ideja
iskreno, otisao sam vise u sirinu nego u samu implementaciju level generatora i search algoritma, pa nisam stigao da to napravim to da lici na nesto
vidim da za level ima vise nacina kako moze da se razvije mreza nodova, ali sam napravio neku basic varijantu i otisao dalje
nisam se trudio da igra izgleda "Juiciness (polish)", al sam se trudio da ubacim sto vise prosirivih sistema, nezavisnih celina
daleko od toga da je sve napravljeno kako treba, ima stvari koje su izuvezane, koje bi trebalo razmirsiti, dosta toga je nekompletno, lako moze da se naleti na scenario koji sistem nece moci da zodovolji (recimo event sistem nema da prosledjuje objekat koji je okino event, nego je zbudzeno), ali poenta je da su nezavisni sistemi, bar je ideja da bi trebalo biti...
ideja je da se jednostavnim prevlacenjem prefaba na scenu aktivira sistem, kao sto je uradjeno za instant notifikacije, gde je developeru jedini zadatak da instancira notification item i da ga posalje sistemu, a sistem sve ostalo sam zavrsi

ovo je moj stil pisanja za mene, ne znaci da bi tako radio u drugom okruzenju, prilagodio bih se zahtevima, naravno
sve sto mogu guram kroz script controllere, dodelim im objekte koji mi trebaju i onda imam glavni skript koji kontrolise sve (dodajem dugme u controller a onda u awake napunim listenere, ne podesavam kroz editor)
ne volim da koristim GetComponent - zato referenciram sve sto mogu
licno volim da kad stavim prefab na scenu da mi je na rootu prefaba odma controller gde cu odmah da vidim koja su mu polja prazna i samo da isprevlacim potrebne instance

namespacing i file striktura nisu srecni uopste, 10x sam menjao kako mi izgleda file tree i nijednom nisam bio zadovoljan
na kraju sam dosao do ideje da sve za nednu "kontrolu", "sistem" stavim u jedan folder, pa kad mi treba za novi projekat samo kopiram folder koji mi treba
al na kraju entity system je za svaku igru poseban, a to je deo system foldera tako da ovako kako je sad za svaki novi projekat moram da prolazim kroz foldere i instanciram stvari koje mi trebaju za taj projekat...
ranije sam imao odvojene foldere "implementation" (core implementacija logike kontrole/sistema) i "instance" (izvedene klase specificne za projekat), ali mi ni to nije zavrsavalo posao... sve razbacano svuda


The pathfinding algorithm should be implemented by the developer (built-in Unity solutions or external packages should not be used)
nadjoh neku implementaciju AStar algoritma, uzo to preradio da odgovara mojim potrebama
trebalo mi i PriorityQueue, e sad posto algoritam "should be implemented by the developer" i "external packages should not be used" dekompajlovao sam Priority Queue.dll i ubacio rucno i sad izgleda ko da sam ga ja pisao :)




nesto lose izgleda popunjavanje puta izmedju nodova - nije glatka animacija... bar ne u game view, u scene view izgleda normalno
fali kad se klikne u prazno da skloni ActionPanel - esc radi poso, al ne moze jednom rukom da se igra :)
mapu ce uvek da centrira, pozicioniranje nije orijentisano prema start nodu


ne moze da se klikne na nodove koji nisu unlockovani. mislim, meni je to potpuno prirodno, ali ne znam dal je ocekivano ponasanje ili ne, nesto mi kroz glavu proslo da treba da moze da se klikce gde god...



to sto svakomalo pise "zbudzeno", "na brazaka" i u tom kontekstu, nije da nisam hteo da se bavim tim i da ga uradim kako treba, nego jednostavno nisam vise imao vremena da se bavim peglanjem jer je bilo jos stvari da pozavrsavam...



tracer ponekad izgleda besmisleno, ali u stvari dobro radi
kada krene hak nekog noda svi okolni nodovi koji su vec hakovani i povezani sa tim koji se hakuje pustaju path da se popunjava
takodje svaki firevall nod se racuna kao da je vec hakovan, tako da cesto pusta linije odatle,ne izgleda normalno, ali u stvari ne radi pogresno
ako jedna instanca tracera zavrsi sa hakovanjem noda, a sledeci nod koji mu je u u ruti se vec hajuje, za taj nod ne pusta punjenje patha vec ceka da se zavrsi zapoceto hakovanje i onda instant predje na taj nod (popuni path)
bilo bi ok da se pusti punjenje patha ali da se normalizuje vreme koliko je ostalo do zavrsetka haka noda... ako stignem


