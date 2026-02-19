package com.example.ajfilfov.service;

import com.example.ajfilfov.entity.User;
import com.example.ajfilfov.entity.UserAddress;
import com.example.ajfilfov.repository.UserRepository;
import com.example.ajfilfov.repository.UserAddressRepository;
import org.springframework.stereotype.Service;
import java.util.*;

@Service
public class UsersService {
    private final UserRepository userRepo;
    private final UserAddressRepository addrRepo;
    public UsersService(UserRepository userRepo, UserAddressRepository addrRepo) {
        this.userRepo = userRepo; this.addrRepo = addrRepo; }
    public List<User> getAllUsers() {
        try { return userRepo.findByIsDeletedFalse(); }
        catch (Exception e) { return userRepo.findAll(); }
    }
    public Optional<User> getUserById(UUID id) { return userRepo.findById(id); }
    public User updateUser(UUID id, User updated) { updated.setIdUtilizator(id); return userRepo.save(updated); }
    public List<UserAddress> getAllUserAddresses() { return addrRepo.findAll(); }
}
