drop schema if exists dead_pigeons cascade;
create schema if not exists dead_pigeons;

-- enable the pgcrypto extension for random UUID generation
create extension if not exists pgcrypto;

create table dead_pigeons.players(
                                     id uuid primary key,
                                     name text not null,
                                     phone text not null,
                                     email text not null,
                                     created_at timestamp not null,
                                     is_active boolean default true not null
);

create table dead_pigeons.users(
                                   id uuid primary key,
                                   name text not null,
                                   password_hash text not null,
                                   role text not null,
                                   created_at timestamp not null
);

create table dead_pigeons.transactions(
                                          id uuid primary key,
                                          player_id uuid references dead_pigeons.players(id) not null,
                                          amount numeric(10,2) not null,
                                          mp_reference text not null unique, -- MobilePay transaction ID
                                          created_at timestamp not null
);

create table dead_pigeons.games(
                                   id uuid primary key,
                                   start_date date not null,
                                   end_date date not null,
                                   winning_numbers int[] check (array_length(winning_numbers,1)=3)
);

create table dead_pigeons.boards(
                                    id uuid primary key,
                                    player_id uuid references dead_pigeons.players(id) not null,
                                    game_id uuid references dead_pigeons.games(id) not null,
                                    number_of_fields int not null check (number_of_fields between 5 and 8), -- 5 to 8 numbers on board
                                    price numeric(10,2) not null, -- 20, 40, 80, or 160 DKK
                                    numbers int[] not null,
                                    created_at timestamp not null
);

create table dead_pigeons.winners(
                                     id uuid primary key,
                                     game_id uuid not null references dead_pigeons.games(id),
                                     player_id uuid not null references dead_pigeons.players(id),
                                     board_id uuid not null references dead_pigeons.boards(id),
                                     winning_amount numeric(10,2) not null,
                                     created_at timestamp not null default now()
);