package com.example.ajfilfov.entity;

import jakarta.persistence.*;
import java.util.UUID;

@Entity
@Table(name = "UserAddresses")
public class UserAddress {
    @Id
    @Column(name = "Id")
    private UUID id;

    @ManyToOne
    @JoinColumn(name = "UserId")
    private User user;

    @Column(name = "StreetAddress")
    private String streetAddress;
    @Column(name = "City")
    private String city;
    @Column(name = "State")
    private String state;
    @Column(name = "ZipCode")
    private String zipCode;
    @Column(name = "Country")
    private String country;

    public UUID getId() { return id; }
    public void setId(UUID id) { this.id = id; }
    public User getUser() { return user; }
    public void setUser(User user) { this.user = user; }
    public String getStreetAddress() { return streetAddress; }
    public void setStreetAddress(String streetAddress) { this.streetAddress = streetAddress; }
    public String getCity() { return city; }
    public void setCity(String city) { this.city = city; }
    public String getState() { return state; }
    public void setState(String state) { this.state = state; }
    public String getZipCode() { return zipCode; }
    public void setZipCode(String zipCode) { this.zipCode = zipCode; }
    public String getCountry() { return country; }
    public void setCountry(String country) { this.country = country; }
}
