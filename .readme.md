## Realizarea unei aplicații web pentru recenzia artiștilor și muzicii lor
### VADER (Valence Aware Dictionary and sEntiment Reasoner) : https://vadersentiment.readthedocs.io/en/latest/index.html

```
use licentaDev
use licentaProd
db.albums.updateMany({},{ $set: { "sentiment": "NoSentiment" } })
db.albums.updateMany({},{ $set: { "reviewCount" : 0 } })
db.albums.updateMany({},{ $set: { "compoundScore" : 0 } })

db.artists.updateMany({},{ $set: { "sentiment": "NoSentiment" } })
db.artists.updateMany({},{ $set: { "reviewCount" : 0 } })
db.artists.updateMany({},{ $set: { "compoundScore" : 0 } })

DELETE

db.artists.updateMany( {}, { $unset: { "rating": 0 } })
```