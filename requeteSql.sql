--Afficher les 5 premiers films ainsi que leurs détails
select Title, ReleaseDate, VoteAverage, Runtime, PosterPath, Name AS "Genre" from Films
    inner join FilmGenre FG on Films.Id = FG.FilmsId
    inner join Genres G on G.Id = FG.GenresId
LIMIT 5;

--Star Wars (plusieurs épisodes),
select * from Films where Title like '%Star Wars%';

--Harrison Ford ou Tom Hanks (acteurs connus dans des films connus à repérer
--facilement dans quelques films. Harrison Ford (dans Star Wars et ApocalypseNow),
select * from Actors where Name = 'Harrison' and Surname = 'Ford';--id = 3
select * from Actors where Name = 'Tom' and Surname = 'Hanks';--id =31

select * from ActorFilm AF inner join Actors A on A.Id = AF.ActorsId
inner join Films F on F.Id = AF.FilmsId
where AF.ActorsId = 3 OR AF.ActorsId = 31;

--Mrs Doubtfire (dans lequel Robin Williams joue plusieurs rôles).
select * from Films where Title like '%Doubtfire%';--id 788

select * from ActorFilm
inner join Actors A on A.Id = ActorFilm.ActorsId
inner join Films F on F.Id = ActorFilm.FilmsId
where FilmsId = (select Id from Films where Title like '%Doubtfire%')
and A.Name = 'Robin' and A.Surname = 'Williams';

--récupère la liste des acteurs d’un film
select * from Actors A
where A.Id IN (select AF.ActorsId from ActorFilm AF where AF.FilmsId = 11);

select * from ActorFilm AF where AF.FilmsId = 11;

--FindListFilmByPartialActorName
select * from Films
inner join ActorFilm AF on Films.Id = AF.FilmsId
inner join Actors A on AF.ActorsId = A.Id
where A.Surname like '%james%' OR A.Name like '%james%';

select * from Actors where Id = 42;