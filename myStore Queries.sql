show databases;

use mystorebd;

create table Clientes (
	idCliente int not null PRIMARY key auto_increment,
	nome Varchar (100) not null,
    email varchar (150) not null UNIQUE,
    telefone varchar (20) null,
    endereco varchar(100) null,
    dataCriacao datetime not null default current_timestamp
);

insert into Clientes 
(nome, email, telefone, endereco)
Values(
'Elton', 'Eltonmirona10@gmail.com', '826910013','Bairro 25 de Junho'
);

show tables;

show columns from Clientes;

select * from Clientes;

SELECT 
	idCliente as 'Id do Cliente',
    nome as 'nome do cliente',
    email,
    telefone,
    endereco,
    dataCriacao as 'Data de Criacao'
From 
	Clientes;
    
update Clientes 
SET  endereco = 'Museu' 
WHERE idCliente = 5;
    

