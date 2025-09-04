package com.example.ajfilfov.entity;

import jakarta.persistence.*;
import java.time.LocalDate;
import java.util.UUID;

@Entity
@Table(name = "Utilizatori")
public class User {
    @Id
    @Column(name = "IdUtilizator")
    private UUID idUtilizator;
    @Column(name = "Nume")
    private String firstName;
    @Column(name = "Prenume")
    private String lastName;
    @Column(name = "DataNastere")
    private LocalDate birthDay;
    @Column(name = "IsDeleted")
    private Boolean isDeleted;
    @Column(name = "NumarTelefon")
    private String numarTelefon;

    public UUID getIdUtilizator() { return idUtilizator; }
    public void setIdUtilizator(UUID idUtilizator) { this.idUtilizator = idUtilizator; }
    public String getFirstName() { return firstName; }
    public void setFirstName(String firstName) { this.firstName = firstName; }
    public String getLastName() { return lastName; }
    public void setLastName(String lastName) { this.lastName = lastName; }
    public LocalDate getBirthDay() { return birthDay; }
    public void setBirthDay(LocalDate birthDay) { this.birthDay = birthDay; }
    public Boolean getIsDeleted() { return isDeleted; }
    public void setIsDeleted(Boolean isDeleted) { this.isDeleted = isDeleted; }
    public String getNumarTelefon() { return numarTelefon; }
    public void setNumarTelefon(String numarTelefon) { this.numarTelefon = numarTelefon; }
}
