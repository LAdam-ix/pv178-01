Hi there, píšem len trochu info o veciach, ktoré som spravil podľa sebe (lebo bolo povedané že si to môźeme trochu upraviť). Ako malo by to byť čitatelné čisto z kódu (aspoň dúfam :D ) ale myslím si, že ti to pojde rýchlejšie opraviť keď k tomu budeš mať aj taký slovný obkec. (Preto to aj píšem radšej "slovensky" než anj nech to je čo najviac zrozumitelné.) 

1. `Projectiles` sú homing na enemy a keď enemy, na ktorú sú zamerané znizne, tak dorazia na jej poslednú pozícu a tam ostanú ako "mines" pokiaľ im nevyprší `lifespan`

2. `Explosive projectiles` vybuchujú aj po tom čo umrú na `lifespan` a nie len pri zásahu. (Nie je k tomu bohužial efekt a nejaká vykreslenie oblasti výbuchu sa mi nepodarilo spraviť, takže to nie je moc dobre vidieť a proste len občas enemy klesnú životy o 10 ak je v range)

3. Basic tower som nerobil vlastnú triedu ale jej funkcionalitu som napísal do `Tower`, ktorú by som normálne premenoval na `BasicTower` a z nej dedil ale kedže bola daná v zadaní tak som to radšej neprepisoval. `Enemy` som dal kaźdú zvláśt lebo majú dosť rozdielne správanie. `Projectile` class bola defaultne abstraktná, tak tam som vytvoril aj prázdnu child classu `BasicProjectile`.

4. Ohľadom toho `OnDrawGizmosSelected` to je viacmenej Unity debug vec kde ti to v scéne vykreslí oblast od objektu v tomto prípade veži (musí byť selecnutá kliknutím). Dobré na kontorlovanie dosahu.
Asi by sa to z komplktného projektu malo zmazať ale nechávam to na lepšiu kontrolu keby ten projekt pozeráś v unity

5. Na discorde bolo spomenuté, že tagy sa nemajú používať an vyhladávanie ale nebolo tam nič proti tomu aby sa použili na identifikovanie objektov, takže dúfam, že to je safe. Prišlo mi to ako najrýchlejšei a celkom aj clean riešenie, ktoré sa dá dobre upravovať do budúcna

6. Detekovanie enemies pri `basic` a `burst tower` by asi išli ešte nejako zjednotiť a vytiahnuť odtiaľ iba tú porovnávaciu funkciu, ale to že majú mať odlišné hladanie enemies som si všimol až pár hodín pred deadlinom, tak dúfam že to takto postači a ak nie, tak za to nebude veľa bodov dole. 